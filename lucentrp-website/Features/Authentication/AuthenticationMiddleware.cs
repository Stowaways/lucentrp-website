using LucentRP.Features.Users;
using LucentRP.Shared.Models.Authentication;
using LucentRP.Shared.Models.User;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using MySql.Data.MySqlClient;

namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// Middleware that will be used to perform user authentication.
    /// </summary>
    public class AuthenticationMiddleware
    {
        /// <summary>
        /// The logger that will be used to log authentication messages.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The database connection that will be used to query user data.
        /// </summary>
        private readonly MySqlConnection _sqlConnection;

        /// <summary>
        /// The RSA key pair that will be used for signing and validating tokens.
        /// </summary>
        private readonly RSAKeyPair _rsaKeyPair;

        /// <summary>
        /// The query that will be used to get user account information from the database.
        /// </summary>
        private readonly IGetUserAccountByID _getUserAccountByID;

        /// <summary>
        /// Create authentication middleware.
        /// </summary>
        /// <param name="serviceProvider">The service provider that will be used
        /// to get services required for user authentication.</param>
        public AuthenticationMiddleware(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger>();
            _sqlConnection = serviceProvider.GetRequiredService<MySqlConnection>();
            _rsaKeyPair = serviceProvider.GetRequiredService<RSAKeyPair>();
            _getUserAccountByID = serviceProvider.GetRequiredService<IGetUserAccountByID>();
        }

        public async Task Authenticate(HttpContext context, Func<Task> next)
        {
            // Get the endpoint that is being requested.
            IEndpointFeature? endpointFeature = context.Features.Get<IEndpointFeature>();
            object? endpointAuthAttribute = null;
            object? endpointAnonAttribute = null;
            object? controllerAuthAttribute = null;
            object? controllerAnonAttribute = null;

            // If the endpoint does not exist.
            if (endpointFeature == null || endpointFeature.Endpoint == null)
            {
                await next();
                return;
            }

            // Get the endpoint's authentication attributes.
            endpointAuthAttribute = endpointFeature.Endpoint.Metadata.Where(m => m is AuthenticateAttribute).FirstOrDefault();
            endpointAnonAttribute = endpointFeature.Endpoint.Metadata.Where(m => m is AnonymousAttribute).FirstOrDefault();

            // Get the controller the endpoint belings to (may not belong to one).
            ControllerActionDescriptor? controller = endpointFeature.Endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();

            // If the endpoint belongs to a controller.
            if (controller != null)
            {

                // Get the class' attributes.
                controllerAuthAttribute = controller.ControllerTypeInfo.GetCustomAttributes(typeof(AuthenticateAttribute), true).FirstOrDefault();
                controllerAnonAttribute = controller.ControllerTypeInfo.GetCustomAttributes(typeof(AnonymousAttribute), true).FirstOrDefault();
            }

            // If the endpoint allows anonoymous requests.
            if (endpointAnonAttribute != null)
            {
                await next();
                return;
            }

            // If the endpoint and the controller do not require authorized requests.
            if (endpointAuthAttribute == null && controllerAuthAttribute == null)
            {
                await next();
                return;
            }

            // If we have not returned yet, that means authentication is required.

            // Get the authentication cookie.
            string? authCookie = context.Request.Cookies["Authorization"];

            // If the authentication cookie was not found.
            if (string.IsNullOrEmpty(authCookie))
            {
                context.Response.StatusCode = 401;
                await context.Response.CompleteAsync();
                return;
            }

            // If the cookie is malformed.
            if(!authCookie.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 400;
                await context.Response.CompleteAsync();
                return;
            }

            // Decode the token.
            string token = authCookie.Replace("Bearer ", "");
            IDictionary<string, object> claims;

            try
            {
                claims = AuthUtilities.DecodeToken(token, _rsaKeyPair.PublicKey, _rsaKeyPair.PrivateKey);
            } catch
            {
                context.Response.StatusCode = 400;
                await context.Response.CompleteAsync();
                return;
            }

            // If the token does not provide the required claims.
            if (claims["id"] == null || claims["password"] == null) {
                context.Response.StatusCode = 401;
                await context.Response.CompleteAsync();
                return;
            }

            // If the claims are provided as the wrong datatype.
            if (claims["id"].GetType() != typeof(long) || claims["password"].GetType() != typeof(string))
            {
                context.Response.StatusCode = 401;
                await context.Response.CompleteAsync();
                return;
            }

            // Load the user account from the database.
            UserAccount? userAccount;

            try
            {
                userAccount = _getUserAccountByID.Execute((long)claims["id"]);
            } catch
            {
                context.Response.StatusCode = 500;
                await context.Response.CompleteAsync();
                return;
            }

            // If the user account could not be found.
            if (userAccount == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.CompleteAsync();
                return;
            }

            // If the passwords do not match.
            if (!userAccount.Password.Equals(claims["password"]))
            {
                context.Response.StatusCode = 401;
                context.Response.Cookies.Delete("Authentication");
                await context.Response.CompleteAsync();
                return;
            }

            // The user has been authenticated.
            context.Items.Add("userAccount", userAccount);
            await next();
        }
    }
}
