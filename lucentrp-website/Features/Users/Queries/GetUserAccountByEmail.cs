using lucentrp.Shared.Models.User;

namespace lucentrp.Features.Users
{
    /// <summary>
    /// A query to get a user account from the database by their email address.
    /// </summary>
    public class GetUserAccountByEmail : IGetUserAccountByEmail
    {
        /// <summary>
        /// The get user account by field query that will used.
        /// </summary>
        private readonly IGetUserAccountByField _getUserAccountByField;

        /// <summary>
        /// Construct a new GetUserAccountByEmail query.
        /// </summary>
        /// <param name="getUserAccountByField">The get user account by field query that will be 
        /// used to query the user by email field.</param>
        public GetUserAccountByEmail(IGetUserAccountByField getUserAccountByField)
        {
            /// <summary>
            /// Get a user account by their email address.
            /// </summary>
            /// <param name="email">The email address of the user account to get.</param>
            /// <returns>The user account if it exists, otherwise null.</returns>
            _getUserAccountByField = getUserAccountByField;
        }

        public UserAccount? Execute(string email)
        {
            return _getUserAccountByField.Execute("email", email);
        }
    }

    /// <summary>
    /// A query to get a user account from the database by their email address.
    /// </summary>
    public interface IGetUserAccountByEmail
    {
        /// <summary>
        /// Get a user account by their email address.
        /// </summary>
        /// <param name="email">The email address of the user account to get.</param>
        /// <returns>The user account if it exists, otherwise null.</returns>
        UserAccount? Execute(string email);
    }
}
