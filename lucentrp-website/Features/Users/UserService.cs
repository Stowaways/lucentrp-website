namespace LucentRP.Features.Users
{
    /// <summary>
    /// A static class that is used to register services required by
    /// the user controller with a service collection.
    /// </summary>
    public static class UserService
    {
        /// <summary>
        /// Register the services required by the user controller.
        /// </summary>
        /// <param name="serviceCollection">The service collection that the services will be registered to.</param>
        public static void Register(IServiceCollection serviceCollection)
        {
            // Managers.
            serviceCollection.AddSingleton<UserAccountManager>();

            // Validators.
            serviceCollection.AddSingleton<UserAccountCreationValidator>();
            serviceCollection.AddSingleton<UserAccountLoginValidator>();
        }
    }
}
