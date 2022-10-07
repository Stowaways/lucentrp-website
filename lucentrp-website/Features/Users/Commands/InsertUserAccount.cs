using Dapper;
using LucentRP.Shared.Models.User;
using MySql.Data.MySqlClient;

namespace LucentRP.Features.Users
{
    /// <summary>
    /// A command to insert user accounts into the database.
    /// </summary>
    public class InsertUserAccount : IInsertUserAccount
    {
        /// <summary>
        /// The connection that will be used to perform the insertion.
        /// </summary>
        private readonly MySqlConnection _sqlConnection;

        /// <summary>
        /// Construct a new InsertUserAccount command.
        /// </summary>
        /// <param name="sqlConnection">The connection that will be used to perform the insertion.</param>
        public InsertUserAccount(MySqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Insert a user account into the database/
        /// </summary>
        /// <param name="userAccount">The user account to insert.</param>
        /// <returns>If the operation was successful or not.</returns>
        public bool Execute(UserAccount userAccount)
        {
            return _sqlConnection.Execute(
                @"INSERT INTO 
                    `user_accounts`
                  (
                    `account_created`,
                    `email`,
                    `username`,
                    `password`,
                    `email_verified`,
                    `password_reset_required`,
                    `account_locked`,
                    `account_banned`
                  )
                  VALUES
                  (
                    @AccountCreated,
                    @Email,
                    @Username,
                    @Password,
                    @EmailIsVerified,
                    @PasswordResetIsRequired,
                    @AccountIsLocked,
                    @AccountIsBanned
                  );",
                new
                {
                    AccountCreated = DateTime.Now,
                    userAccount.Email,
                    userAccount.Username,
                    userAccount.Password,
                    EmailIsVerified = false,
                    PasswordResetIsRequired = false,
                    AccountIsLocked = false,
                    AccountIsBanned = false
                }
            ) > 0;
        }
    }

    /// <summary>
    /// A command to insert user accounts into the database.
    /// </summary>
    public interface IInsertUserAccount
    {
        /// <summary>
        /// Insert a user account into the database/
        /// </summary>
        /// <param name="userAccount">The user account to insert.</param>
        /// <returns>If the operation was successful or not.</returns>
        bool Execute(UserAccount userAccount);
    }
}
