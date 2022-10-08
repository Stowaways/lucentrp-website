using LucentRP.Shared.Models.User;
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
        /// The authenticate command that will be used to authenticate users.
        /// </summary>
        private readonly IAuthenticate _authenticate;

        /// <summary>
        /// Create authentication middleware.
        /// </summary>
        /// <param name="authenticate">The authenticate command that will be used
        /// to authenticate users.</param>
        public AuthenticationMiddleware(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        /// <summary>
        /// Execute the authentication.
        /// </summary>
        /// <param name="context">The request context.</param>
        /// <param name="next">The next middleware function to call.</param>
        /// <returns>A task.</returns>
        public async Task Execute(HttpContext context, Func<Task> next)
        {
            // If authentication is not required.
            if (!AuthenticationIsRequired(context))
            {
                await next();
                return;
            }

            // Get the authentication cookie.
            string? authCookie = context.Request.Cookies["Authorization"];

            // If there is no valid auth cookie.
            if (string.IsNullOrEmpty(authCookie) || !authCookie.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                await context.Response.CompleteAsync();
                return;
            }

            // Authenticate the user.
            UserAccount? userAccount;

            try
            {
                userAccount = _authenticate.Execute(authCookie.Replace("Bearer ", ""));
            }
            catch (Exception exception)
            {
                context.Response.StatusCode = exception is MySqlException ? 500 : 401;
                await context.Response.CompleteAsync();
                return;
            }

            // If the authentication failed.
            if (userAccount is null)
            {
                context.Response.StatusCode = 401;
                await context.Response.CompleteAsync();
                return;
            }

            // If the authentication succeeded.
            context.Items.Add("account", userAccount);
            await next();
        }

        /// <summary>
        /// Check if authentication is required on a request.
        /// </summary>
        /// <param name="context">The request context.</param>
        /// <returns>If authentication is required or not.</returns>
        private static bool AuthenticationIsRequired(HttpContext context)
        {
            // Get the endpoint that is being requested.
            Endpoint? endpoint = context.GetEndpoint();

            // If the endpoint does not exist.
            if (endpoint is null)
                return false;

            // Get the endpoint's authentication attributes.
            object? endpointAuthAttribute = endpoint.Metadata.FirstOrDefault(m => m is AuthenticateAttribute);
            object? endpointAnonAttribute = endpoint.Metadata.FirstOrDefault(m => m is AnonymousAttribute);

            // Get the controller the endpoint belings to (may not belong to one).
            ControllerActionDescriptor? controller = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            object? controllerAuthAttribute = null;

            // If the endpoint belongs to a controller.
            if (controller is not null)
                controllerAuthAttribute = controller.ControllerTypeInfo.GetCustomAttributes(typeof(AuthenticateAttribute), true).FirstOrDefault();

            // If the endpoint allows anonoymous requests.
            if (endpointAnonAttribute is not null)
                return false;

            return endpointAuthAttribute is not null || controllerAuthAttribute is not null;
        }
    }
}
