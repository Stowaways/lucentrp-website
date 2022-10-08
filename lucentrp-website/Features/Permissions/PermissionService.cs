namespace LucentRP.Features.Permissions
{
    /// <summary>
    /// A static class that is used to register services required by
    /// the permission controller with a service collection.
    /// </summary>
    public static class PermissionService
    {
        /// <summary>
        /// Register the services required by the permission controller.
        /// </summary>
        /// <param name="serviceCollection">The service collection that the services will be registered to.</param>
        public static void Register(IServiceCollection serviceCollection)
        {
            // Managers.
            serviceCollection.AddSingleton<PermissionCategoryManager>();
        }
    }
}
