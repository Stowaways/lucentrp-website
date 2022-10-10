namespace LucentRP.Features.Authorization
{
    /// <summary>
    /// A static class that is used to register services required
    /// for user authorization.
    /// </summary>
    public static class AuthorizationService
    {
        /// <summary>
        /// Register the services required for user authorization.
        /// </summary>
        /// 
        /// <param name="serviceCollection">The service collection the services will be registered to.</param>
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAuthorizeAny, AuthorizeAny>();
            serviceCollection.AddSingleton<IAuthorizeAll, AuthorizeAll>();
        }
    }
}
