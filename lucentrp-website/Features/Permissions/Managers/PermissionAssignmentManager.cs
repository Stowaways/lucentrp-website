using Dapper;
using LucentRP.Shared.DataManager;
using LucentRP.Shared.Models.Permission;
using LucentRP.Shared.Models.User;
using MySql.Data.MySqlClient;

namespace LucentRP.Features.Permissions
{
    /// <summary>
    /// A manager to manager permission assignments in the database.
    /// </summary>
    public class PermissionAssignmentManager : AbstractDataManager<PermissionAssignment>
    {
        /// <summary>
        /// Construct a new PermissionAssignmentManager.
        /// </summary>
        /// <param name="sqlConnection">The connection that will be used to manage permission assignments.</param>
        public PermissionAssignmentManager(MySqlConnection sqlConnection) : base(sqlConnection)
        {
        }

        /// <summary>
        /// Insert a permission assignment into the database.
        /// </summary>
        /// <param name="assignment">The assignment to insert.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Insert(PermissionAssignment assignment)
        {
            return sqlConnection.Execute(
                @"INSERT INTO
                    `permission_assignments`
                  (
                    `id`,
                    `user_account_id`,
                    `permission_node_id`
                  )
                  VALUES
                  (
                    @ID,
                    @UserID,
                    @PermissionID
                  )",
                assignment
            ) > 0;
        }

        /// <summary>
        /// Update a permission assignment in the database.
        /// </summary>
        /// <param name="assignment">The assignment to update.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Update(PermissionAssignment assignment)
        {
            return sqlConnection.Execute(
                @"UPDATE
                    `permission_assignments`
                  SET
                    `user_account_id` = @ID,
                    `permission_node_id` = @PermissionID
                  WHERE
                    `id` = @ID",
                assignment
            ) > 0;
        }

        /// <summary>
        /// Get a permission assignment.
        /// </summary>
        /// <param name="assignment">The permission assignment to get.</param>
        /// <returns>The permission assignment.</returns>
        public override PermissionAssignment? Get(PermissionAssignment assignment)
        {
            return sqlConnection.Query<PermissionAssignment>(
                @"SELECT
                    `id` as ID,
                    `user_account_id` as UserID,
                    `permission_node_id` as PermissionID
                  FROM
                    `permission_assignments`
                  WHERE
                    `id` = @ID",
                assignment
            ).FirstOrDefault();
        }

        /// <summary>
        /// Get all of the permission assignments belonging to a user.
        /// </summary>
        /// <param name="userAccount">The user.</param>
        /// <returns>The permission assignments belonging to the user.</returns>
        public IEnumerable<PermissionAssignment> GetUserPermissionAssignments(UserAccount userAccount)
        {
            return sqlConnection.Query<PermissionAssignment>(
                @"SELECT
                    `id` as ID,
                    `user_account_id` as UserID,
                    `permission_node_id` as PermissionID
                  FROM
                    `permission_assignments`
                  WHERE
                    `user_account_id` = @ID",
                userAccount
            );
        }

        /// <summary>
        /// Get all of the permission nodes that have been assigned to a user.
        /// </summary>
        /// <param name="userAccount">The user.</param>
        /// <returns>The permission nodes that have been assigned to the user.</returns>
        public IEnumerable<PermissionNode> GetUserPermissionNodes(UserAccount userAccount)
        {
            return sqlConnection.Query<PermissionNode>(
                @"SELECT
                    `id`,
                    `category_id`,
                    `name`
                  FROM
                    `permission_nodes`
                  WHERE
                    `id` IN
                    (
                        SELECT
                            `id` as ID
                        FROM
                            `permission_assignments`
                        WHERE
                            `user_account_id` = @ID 
                    )",
                userAccount
            );
        }

        /// <summary>
        /// Delete a permission assignment in the database.
        /// </summary>
        /// <param name="assignment">The permission assignment to delete.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Delete(PermissionAssignment assignment)
        {
            return sqlConnection.Execute(
                @"DELETE FROM
                    `permission_assignments`
                  WHERE
                    `id` = @ID",
                assignment
            ) > 0;
        }
    }
}
