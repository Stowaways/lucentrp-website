using MySql.Data.MySqlClient;

namespace LucentRP.Shared.DataManager
{
    /// <summary>
    /// An abstract class that where implementing classes are used to manages columns in a database.
    /// </summary>
    /// <typeparam name="T">The column data type.</typeparam>
    public abstract class AbstractDataManager<T> : IColumnManager<T>
    {
        /// <summary>
        /// The connection that will be used by the manager.
        /// </summary>
        protected readonly MySqlConnection sqlConnection;

        /// <summary>
        /// Construct a new AbstractColumnManager
        /// </summary>
        /// <param name="sqlConnection"></param>
        protected AbstractDataManager(MySqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Insert a column.
        /// </summary>
        /// <param name="obj">The column to insert.</param>
        /// <returns>If the operation was successful or not.</returns>
        public abstract bool Delete(T obj);

        /// <summary>
        /// Update a column.
        /// </summary>
        /// <param name="obj">The column to update.</param>
        /// <returns>If the operation was successful or not.</returns>
        public abstract bool Insert(T obj);

        /// <summary>
        /// Query a column.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The result of the query..</returns>
        public abstract T? Query(T obj);

        /// <summary>
        /// Delete a column.
        /// </summary>
        /// <param name="obj">The column to delete.</param>
        /// <returns>If the operation was successful or not.</returns>
        public abstract bool Update(T obj);
    }

    /// <summary>
    /// An interface that manages columns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IColumnManager<T>
    {
        /// <summary>
        /// Insert a column.
        /// </summary>
        /// <returns>If the operation was successful or not.</returns>
        bool Insert(T obj);

        /// <summary>
        /// Update a column.
        /// </summary>
        /// <returns>If the operation was successful or not.</returns>
        bool Update(T obj);

        /// <summary>
        /// Query a column.
        /// </summary>
        /// <returns>The result of the query.</returns>
        T? Query(T obj);

        /// <summary>
        /// Delete a column.
        /// </summary>
        /// <returns>If the operation was successful or not.</returns>
        bool Delete(T obj);
    }
}
