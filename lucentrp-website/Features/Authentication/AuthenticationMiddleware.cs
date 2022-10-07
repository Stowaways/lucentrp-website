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
        /// The authentication middleware implementation.
        /// </summary>
        /// <param name="context">The request context.</param>
        /// <param name="next">The next middleware function to call.</param>
        /// <returns>A task.</returns>
        public async Task Authenticate(HttpContext context, Func<Task> next)
        {
            if (AuthenticationIsRequired(context))
            {
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
                    userAccount = _authenticate.Execute(authCookie);
                } catch (Exception exception)
                {
                    context.Response.StatusCode = exception is MySqlException ? 500 : 401;
                    await context.Response.CompleteAsync();
                    return;
                }

                // If the authentication failed.
                if (userAccount == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.CompleteAsync();
                    return;
                }

                // If the authentication succeeded.
                context.Items.Add("account", userAccount);
            }

            // Call the next middleware function.
            await next();
        }

        /// <summary>
        /// Check if authentication is required on a request.
        /// </summary>
        /// <param name="context">The request context.</param>
        /// <returns>If authentication is required or not.</returns>
        private bool AuthenticationIsRequired(HttpContext context)
        {
            // Get the endpoint that is being requested.
            IEndpointFeature? endpointFeature = context.Features.Get<IEndpointFeature>();
            object? endpointAuthAttribute = null;
            object? endpointAnonAttribute = null;
            object? controllerAuthAttribute = null;
            object? controllerAnonAttribute = null;

            // If the endpoint does not exist.
            if (endpointFeature == null || endpointFeature.Endpoint == null)
                return false;

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
                return false;

            // If the endpoint and the controller do not require authorized requests.
            if (endpointAuthAttribute == null && controllerAuthAttribute == null)
                return false;

            // Authentication is required.
            return true;
        }
    }
}
