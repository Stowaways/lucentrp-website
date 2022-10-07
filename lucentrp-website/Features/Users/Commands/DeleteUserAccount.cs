using Dapper;
using MySql.Data.MySqlClient;

namespace LucentRP.Features.Users
{
    /// <summary>
    /// A command to delete user accounts from the database.
    /// </summary>
    public class DeleteUserAccount : IDeleteUserAccount
    {
        /// <summary>
        /// The connection that will be used to perform the deletion.
        /// </summary>
        private readonly MySqlConnection _sqlConnection;

        /// <summary>
        /// Construct a new DeleteUserAccount command.
        /// </summary>
        /// <param name="sqlConnection">The connection that will be used to perform the deletions.</param>
        public DeleteUserAccount(MySqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Delete a user account from the database.
        /// </summary>
        /// <param name="id">The id of the user account to delete.</param>
        /// <returns>If the operation was successful or not.</returns>
        public bool Execute(long id)
        {
            return _sqlConnection.Execute(
                @"DELETE FROM
                    `user_accounts`
                  WHERE
                    `id` = @id;",
                  new
                  {
                      id
                  }
            ) > 0;
        }
    }

    /// <summary>
    /// A command to delete user accounts from the database.
    /// </summary>
    public interface IDeleteUserAccount
    {
        /// <summary>
        /// Delete a user account from the database.
        /// </summary>
        /// <param name="id">The id of the user account to delete.</param>
        /// <returns>If the operation was successful or not.</returns>
        bool Execute(long id);
    }
}
