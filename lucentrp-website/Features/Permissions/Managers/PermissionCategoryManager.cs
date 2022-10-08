using Dapper;
using LucentRP.Shared.DataManager;
using LucentRP.Shared.Models.Permission;
using MySql.Data.MySqlClient;

namespace LucentRP.Features.Permissions
{
    /// <summary>
    /// A manager to manager permission categories in the database.
    /// </summary>
    public class PermissionCategoryManager : AbstractDataManager<PermissionCategory>
    {
        /// <summary>
        /// Construct a PermissionCategoryManager.
        /// </summary>
        /// <param name="sqlConnection">The connection that will be used to manager permission categories.</param>
        public PermissionCategoryManager(MySqlConnection sqlConnection) : base(sqlConnection)
        {
        }

        /// <summary>
        /// Insert a permission category.
        /// </summary>
        /// <param name="category">The permission category to insert.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Insert(PermissionCategory category)
        {
            return sqlConnection.Execute(
                @"INSERT INTO
                    `permission_categories`
                  (
                    `id`,
                    `name`
                  )
                  VALUES
                  (
                    @ID,
                    @Name
                  )",
                category
            ) > 0;
        }

        /// <summary>
        /// Update a permission category.
        /// </summary>
        /// <param name="category">The permission category to update.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Update(PermissionCategory category)
        {
            return sqlConnection.Execute(
                @"UPDATE
                    `permission_categories`
                  SET
                    `name` = @Name
                  WHERE
                    `id` = @ID",
                category
            ) > 0;
        }

        /// <summary>
        /// Get a permission category.
        /// </summary>
        /// <param name="category">The permission category to get.</param>
        /// <returns>The permission category.</returns>
        public override PermissionCategory? Query(PermissionCategory category)
        {
            return sqlConnection.Query<PermissionCategory>(
                @"SELECT
                    `id` as ID,
                    `name` as Name
                  FROM 
                    `permission_categories`
                  WHERE
                    `id` = @ID",
                category
            ).FirstOrDefault();
        }

        /// <summary>
        /// Get all permission categories.
        /// </summary>
        /// <returns>All of the permission categories.</returns>
        public IEnumerable<PermissionCategory> GetAllCategories()
        {
            return sqlConnection.Query<PermissionCategory>(
                @"SELECT
                    `id` as ID,
                    `name` as Name
                  FROM 
                    `permission_categories`"
            );
        }

        /// <summary>
        /// Delete a permission category.
        /// </summary>
        /// <param name="category">The permission category to delete.</param>
        /// <returns>If the operation was successful or not.</returns>
        public override bool Delete(PermissionCategory category)
        {
            return sqlConnection.Execute(
                @"DELETE FROM
                    `permission_categories`
                  WHERE
                    `id` = @ID",
                category
            ) > 0;
        }
    }
}
