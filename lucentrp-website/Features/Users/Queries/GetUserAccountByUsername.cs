using LucentRP.Shared.Models.User;

namespace LucentRP.Features.Users
{
    /// <summary>
    /// A query to get a user account from the database by their user username.
    /// </summary>
    public class GetUserAccountByUsername : IGetUserAccountByUsername
    {
        /// <summary>
        /// The get user account by field query that will used.
        /// </summary>
        private readonly IGetUserAccountByField _getUserAccountByField;

        /// <summary>
        /// Construct a new GetUserAccountByUsername.
        /// </summary>
        /// <param name="getUserAccountByField">The get user account by field query that will used.</param>
        public GetUserAccountByUsername(IGetUserAccountByField getUserAccountByField)
        {
            _getUserAccountByField = getUserAccountByField;
        }

        /// <summary>
        /// Get a user account by their username.
        /// </summary>
        /// <param name="username">The username of the user account to get.</param>
        /// <returns>The user account.</returns>
        public UserAccount? Execute(string username)
        {
            return _getUserAccountByField.Execute("username", username);
        }
    }

    /// <summary>
    /// A query to get a user account from the database by their user username.
    /// </summary>
    public interface IGetUserAccountByUsername
    {
        /// <summary>
        /// Get a user account by their username.
        /// </summary>
        /// <param name="username">The username of the user account to get.</param>
        /// <returns>The user account.</returns>
        UserAccount? Execute(string username);
    }
}
