using Dapper;
using LucentRP.Shared.DataManager;
using LucentRP.Shared.Models.Permission;
using MySql.Data.MySqlClient;

namespace LucentRP.Features.Permissions
{
    /// <summary>
    /// A manager to manager permission nodes in the database.
    /// </summary>
    public class PermissionNodeManager : AbstractDataManager<PermissionNode>
    {
        /// <summary>
        /// Construct a new PermissionNodeManager.
        /// </summary>
        /// <param name="sqlConnection">The connection that will be used to manage permission nodes.</param>
        public PermissionNodeManager(MySqlConnection sqlConnection) : base(sqlConnection)
        {
        }

        /// <summary>
        /// Insert a permission node into the database.
        /// </summary>
        /// <param name="permissionNode">The permission node to insert.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Insert(PermissionNode permissionNode)
        {
            return sqlConnection.Execute(
                @"INSERT INTO
                    `permission_nodes`
                  (
                    `id`,
                    `category_id`,
                    `name`
                  )
                  VALUES
                  (
                    @ID,
                    @CategoryID,
                    @Name
                  )",
                permissionNode
            ) > 0;
        }

        /// <summary>
        /// Update a permission node in the database.
        /// </summary>
        /// <param name="permissionNode">The permission node to update.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Update(PermissionNode permissionNode)
        {
            return sqlConnection.Execute(
                @"UPDATE
                    `permission_nodes`
                  SET
                    category_id = @CategoryID,
                    name = @Name
                  WHERE
                    id = @ID",
                permissionNode
            ) > 0;
        }

        /// <summary>
        /// Query a permission node from the database.
        /// </summary>
        /// <param name="permissionNode">The permission node to query.</param>
        /// <returns>The result of the query.</returns>
        public override PermissionNode? Query(PermissionNode permissionNode)
        {
            return sqlConnection.Query<PermissionNode>(
                @"SELECT
                    `id` as ID,
                    `category_id` as CategoryID,
                    `name` as Name
                  FROM
                    `permission_nodes`
                  WHERE
                    `id` = @ID",
                permissionNode
            ).FirstOrDefault();
        }

        /// <summary>
        /// Get all permission nodes in the database.
        /// </summary>
        /// <returns>All of the permission nodes.</returns>
        public IEnumerable<PermissionNode> GetAllPermissionNodes()
        {
            return sqlConnection.Query<PermissionNode>(
                @"SELECT
                    `id` as ID,
                    `category_id` as CategoryID,
                    `name` as Name
                  FROM
                    `permission_nodes`"
            );
        }

        /// <summary>
        /// Delete a permission node in the database.
        /// </summary>
        /// <param name="permissionNode">The permission node to delete.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Delete(PermissionNode permissionNode)
        {
            return sqlConnection.Execute(
                @"DELETE FROM
                    `permission_nodes`
                  WHERE
                    id = @ID",
                permissionNode
            ) > 0;
        }
    }
}
