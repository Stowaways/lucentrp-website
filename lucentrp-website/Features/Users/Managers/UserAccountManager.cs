using Dapper;
using LucentRP.Shared.DataManager;
using LucentRP.Shared.Models.User;
using MySql.Data.MySqlClient;

namespace LucentRP.Features.Users
{
    /// <summary>
    /// A manager to manager user accounts in the databse.
    /// </summary>
    public class UserAccountManager : AbstractDataManager<UserAccount>
    {
        /// <summary>
        /// Construct a new UserAccountManager.
        /// </summary>
        /// <param name="sqlConnection">The connection that will be used to manager user accounts.</param>
        public UserAccountManager(MySqlConnection sqlConnection) : base(sqlConnection)
        {
        }

        /// <summary>
        /// Insert a user account into the database.
        /// </summary>
        /// <param name="userAccount">The user account to insert.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Insert(UserAccount userAccount)
        {
            return sqlConnection.Execute(
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

        /// <summary>
        /// Update a user account in the database.
        /// </summary>
        /// <param name="userAccount">The user account to update.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Update(UserAccount userAccount)
        {
            return sqlConnection.Execute(
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

        public UserAccount? GetByField(string field, object value)
        {
            return sqlConnection.Query<UserAccount>(
                @$"SELECT
                    `id` as ID,
                    `account_created` as AccountCreated,
                    `email` as Email,
                    `username` as Username,
                    `password` as Password,
                    `email_verified` as EmailIsVerified,
                    `password_reset_required` as PasswordResetIsRequired,
                    `account_locked` as AccountIsLocked,
                    `account_banned` as AccountIsBanned
                  FROM
                    `user_accounts`
                  WHERE
                    `{field}` = @value",
                new
                {
                    value
                }
            ).FirstOrDefault();
        }

        /// <summary>
        /// Get a user account from the database.
        /// </summary>
        /// <param name="userAccount">The user account to get from the database.</param>
        /// <returns>The resut of the query.</returns>
        public override UserAccount? Get(UserAccount userAccount)
        {
            return GetByField("id", userAccount.ID);
        }

        /// <summary>
        /// Get a user account from the database by their id.
        /// </summary>
        /// <param name="id">The id of the user account.</param>
        /// <returns>The result of the query.</returns>
        public UserAccount? GetByID(long id)
        {
            return GetByField("id", id);
        }

        /// <summary>
        /// Get a user account from the database by their username.
        /// </summary>
        /// <param name="username">The username of the user account.</param>
        /// <returns>The result of the query.</returns>
        public UserAccount? GetByUsername(string username)
        {
            return GetByField("username", username);
        }

        /// <summary>
        /// Get a user account from the database by their email.
        /// </summary>
        /// <param name="email">The email of the user account.</param>
        /// <returns>The result of the query.</returns>
        public UserAccount? GetByEmail(string email)
        {
            return GetByField("email", email);
        }

        /// <summary>
        /// Delete a user account from the database.
        /// </summary>
        /// <param name="userAccount">The user account to delete.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Delete(UserAccount userAccount)
        {
            return sqlConnection.Execute(
                @"DELETE FROM
                    `user_accounts`
                  WHERE
                    `id` = @id;",
                userAccount
            ) > 0;
        }
    }
}
