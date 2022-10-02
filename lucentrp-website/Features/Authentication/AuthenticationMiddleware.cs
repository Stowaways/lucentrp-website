using Microsoft.AspNetCore.Http.Features;
using MySql.Data.MySqlClient;

namespace lucentrp.Features.Authentication
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
        /// Create authentication middleware.
        /// </summary>
        /// <param name="serviceProvider">The service provider that will be used
        /// to get services required for user authentication.</param>
        public AuthenticationMiddleware(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger>();
            _sqlConnection = serviceProvider.GetRequiredService<MySqlConnection>();
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

            // If the endpoint belongs to a class.
            if (endpointFeature.Endpoint.RequestDelegate != null && endpointFeature.Endpoint.RequestDelegate.Method.DeclaringType != null)
            {
                // Get the declaring class.
                Type declaringType = endpointFeature.Endpoint.RequestDelegate.Method.DeclaringType;

                // Get the class' attributes.
                controllerAuthAttribute = declaringType.GetCustomAttributes(typeof(AuthenticateAttribute), true).FirstOrDefault();
                controllerAnonAttribute = declaringType.GetCustomAttributes(typeof(AnonymousAttribute), true).FirstOrDefault();
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

            // TODO: Authorization
            await next();
        }
    }
}
