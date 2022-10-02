using lucentrp.Shared.Models.User;
using MySql.Data.MySqlClient;
using Dapper;

namespace lucentrp.Features.Users
{
    /// <summary>
    /// Get a user from the database based on a field and value.
    /// </summary>
    public class GetUserAccountByField : IGetUserAccountByField
    {
        /// <summary>
        /// The connection that will be used to perform the query.
        /// </summary>
        private readonly MySqlConnection _sqlConnection;

        /// <summary>
        /// Construct a new GetUserByField query.
        /// </summary>
        /// <param name="sqlConnection">The sql connection that will be used to perform the query.</param>
        public GetUserAccountByField(MySqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Get a user account from the database by a field.
        /// </summary>
        /// 
        /// <remarks>
        /// The field should never be provided by the client,
        /// if it is, they can perform an sql injection.
        /// </remarks>
        /// 
        /// <param name="field">The field to get the user account by.</param>
        /// <param name="value">The value the field must equal.</param>
        /// <returns>The user account.</returns>
        public UserAccount? Execute(string field, object value)
        {
            IEnumerable<UserAccount> accounts = _sqlConnection.Query<UserAccount>(
                @$"SELECT
                    `id` as ID,
                    `account_created` as AccountCreated,
                    `email` as Email,
                    `username` as Username,
                    `password` as Password
                  FROM
                    `user_accounts`
                  WHERE
                    `{field}` = @value",
                new
                {
                    value
                }
            );

            return accounts.Any() ? accounts.First() : null;
        }
    }

    /// <summary>
    /// Get a user from the database based on a field and value.
    /// </summary>
    public interface IGetUserAccountByField
    {
        /// <summary>
        /// Get a user account from the database by a field.
        /// </summary>
        /// 
        /// <remarks>
        /// The field should never be provided by the client,
        /// if it is, they can perform an sql injection.
        /// </remarks>
        /// 
        /// <param name="field">The field to get the user account by.</param>
        /// <param name="value">The value the field must equal.</param>
        /// <returns>The user account.</returns>
        UserAccount? Execute(string field, object value);
    }
}
