using LucentRP.Shared.Models.User;
using MySql.Data.MySqlClient;
using Dapper;

namespace LucentRP.Features.Users
{
    /// <summary>
    /// A command to update a user account in the database.
    /// </summary>
    public class UpdateUserAccount : IUpdateUserAccount
    {
        /// <summary>
        /// The connection that will be used to perform the update.
        /// </summary>
        private readonly MySqlConnection _sqlConnection;

        /// <summary>
        /// Construct a new UpdateUserAccount command.
        /// </summary>
        /// <param name="sqlConnection">The connection that will be used to perform the update.</param>
        public UpdateUserAccount(MySqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Update a user account in the database.
        /// </summary>
        /// <param name="userAccount">The user account to update.</param>
        /// <returns>If the operation was successful or not.</returns>
        public bool Execute(UserAccount userAccount)
        {
            return _sqlConnection.Execute(
                @"UPDATE
                    `user_accounts`
                  SET
                    `email` = @Email,
                    `username` = @Username,
                    `password` = @Password,
                    `email_verified` = @EmailIsVerified,
                    `password_reset_required` = @PasswordResetIsRequired,
                    `account_locked` = @AccountIsLocked,
                    `account_banned` = @AccountIsBanned
                  WHERE
                    `id` = @ID;",
                userAccount
            ) > 0;
        }
    }

    /// <summary>
    /// A command to update a user account in the database.
    /// </summary>
    public interface IUpdateUserAccount
    {
        /// <summary>
        /// Update a user account in the database.
        /// </summary>
        /// <param name="userAccount">The user account to update.</param>
        /// <returns>If the operation was successful or not.</returns>
        bool Execute(UserAccount userAccount);
    }
}
