using LucentRP.Shared.Models.User;

namespace LucentRP.Features.Users
{
    /// <summary>
    /// A query to get a user account from the database by their user id.
    /// </summary>
    public class GetUserAccountByID : IGetUserAccountByID
    {
        /// <summary>
        /// The get user account by field query that will used.
        /// </summary>
        private readonly IGetUserAccountByField _getUserAccountByField;

        /// <summary>
        /// Construct a new GetUserAccountByID query.
        /// </summary>
        /// <param name="getUserAccountByField">The get user account by field query that will used.</param>
        public GetUserAccountByID(IGetUserAccountByField getUserAccountByField)
        {
            _getUserAccountByField = getUserAccountByField;
        }

        /// <summary>
        /// Get a user account by their id.
        /// </summary>
        /// <param name="id">The id of the user account to get.</param>
        /// <returns>The user account.</returns>
        public UserAccount? Execute(long id)
        {
            return _getUserAccountByField.Execute("id", id);
        }
    }

    /// <summary>
    /// A query to get a user account from the database by their user id.
    /// </summary>
    public interface IGetUserAccountByID
    {
        /// <summary>
        /// Get a user account by their id.
        /// </summary>
        /// <param name="id">The id of the user account to get.</param>
        /// <returns>The user account.</returns>
        UserAccount? Execute(long id);
    }
}
