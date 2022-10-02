namespace lucentrp.Features.Users
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
            // Commands.
            serviceCollection.AddSingleton<IInsertUserAccount, InsertUserAccount>();
            serviceCollection.AddSingleton<IUpdateUserAccount, UpdateUserAccount>();
            serviceCollection.AddSingleton<IDeleteUserAccount, DeleteUserAccount>();

            // Queries.
            serviceCollection.AddSingleton<IGetUserAccountByField, GetUserAccountByField>();
            serviceCollection.AddSingleton<IGetUserAccountByID, GetUserAccountByID>();
            serviceCollection.AddSingleton<IGetUserAccountByUsername, GetUserAccountByUsername>();

            // Validators.
            serviceCollection.AddSingleton<UserAccountLoginValidator>();
        }
    }
}
