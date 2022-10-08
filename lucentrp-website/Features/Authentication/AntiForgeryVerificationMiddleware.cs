using Microsoft.AspNetCore.Mvc.Controllers;

namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// Middleware that will be used to prevent cross-site request forgery.
    /// </summary>
    public class AntiForgeryVerificationMiddleware
    {
        /// <summary>
        /// The command that will be used to perform the verification.
        /// </summary>
        private readonly IVerifyAntiForgeryToken _verifyAntiForgeryToken;

        /// <summary>
        /// Create anti forgery middlware.
        /// </summary>
        /// <param name="verifyAntiForgeryToken">The command that will be used to
        /// perform the verification.</param>
        public AntiForgeryVerificationMiddleware(IVerifyAntiForgeryToken verifyAntiForgeryToken)
        {
            _verifyAntiForgeryToken = verifyAntiForgeryToken;
        }

        /// <summary>
        /// Execute the verification.
        /// </summary>
        /// <param name="context">The request context.</param>
        /// <param name="next">The next middleware function to call.</param>
        /// <returns>A task.</returns>
        public async Task Execute(HttpContext context, Func<Task> next)
        {
            // If verification is not required.
            if (!VerificationIsRequired(context))
            {
                await next();
                return;
            }

            // Get the authentication cookie.
            string? authCookie = context.Request.Cookies["Authorization"];
            string? csrfToken = context.Request.Headers["CsrfToken"];

            // If there is no valid auth cookie.
            if (string.IsNullOrEmpty(authCookie) || !authCookie.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                await context.Response.CompleteAsync();
                return;
            }

            // If no csrf token was specified.
            if (string.IsNullOrEmpty(csrfToken))
            {
                context.Response.StatusCode = 400;
                await context.Response.CompleteAsync();
                return;
            }

            // If anti-forgery could not be verified.
            if (!_verifyAntiForgeryToken.Execute(authCookie.Replace("Bearer ", ""), csrfToken))
            {
                context.Response.StatusCode = 400;
                await context.Response.CompleteAsync();
                return;
            }

            await next();
        }

        /// <summary>
        /// Check if anti-forgery verification is required on a request.
        /// </summary>
        /// <param name="context">The request context.</param>
        /// <returns>If verification is required or not.</returns>
        private static bool VerificationIsRequired(HttpContext context)
        {
            // Get the endpoint that is being requested.
            Endpoint? endpoint = context.GetEndpoint();

            // If the endpoint does not exist.
            if (endpoint is null)
                return false;

            // Get the endpoint's verification attributes.
            object? endpointVerificationAttribute = endpoint.Metadata.FirstOrDefault(m => m is AntiForgeryAttribute);

            // Get the controller the endpoint belings to (may not belong to one).
            ControllerActionDescriptor? controller = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            object? controllerVerificationAttribute = null;

            // If the endpoint belongs to a controller.
            if (controller is not null)
                controllerVerificationAttribute = controller.ControllerTypeInfo.GetCustomAttributes(typeof(AntiForgeryAttribute), true).FirstOrDefault();

            return endpointVerificationAttribute is not null || controllerVerificationAttribute is not null;
        }
    }
}
