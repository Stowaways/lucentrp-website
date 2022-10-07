using LucentRP.Shared.Models.Authentication;

namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// A static class that is used to register services required by
    /// for user authentication.
    /// </summary>
    public class AuthenticationService
    {
        /// <summary>
        /// Register the services required for user authentication.
        /// </summary>
        /// <param name="serviceCollection">The service collection the services will be registered to.</param>
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAuthenticate, Authenticate>();
            serviceCollection.AddSingleton(serviceProvider => new AuthenticationMiddleware(serviceProvider.GetRequiredService<IAuthenticate>()));
            serviceCollection.AddSingleton(serviceProvider => new TokenManager(serviceProvider.GetRequiredService<RSAKeyPair>()));
            serviceCollection.AddSingleton(serviceProvider => 
                new RSAKeyPair(
                    AuthUtilities.LoadKey(
                        Path.Combine(
                            Directory.GetCurrentDirectory(), 
                            serviceProvider.GetRequiredService<IConfigurationRoot>()["Authentication:PublicKey"]
                        )
                    ),
                    AuthUtilities.LoadKey(
                        Path.Combine(
                            Directory.GetCurrentDirectory(), 
                            serviceProvider.GetRequiredService<IConfigurationRoot>()["Authentication:PrivateKey"]
                        )
                    )
                )
            );
        }
    }
}
