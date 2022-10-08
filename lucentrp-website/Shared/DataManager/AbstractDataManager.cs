using MySql.Data.MySqlClient;

namespace LucentRP.Shared.DataManager
{
    /// <summary>
    /// An abstract class that where implementing classes are used to manages rows in a database.
    /// </summary>
    /// <typeparam name="T">The row data type.</typeparam>
    public abstract class AbstractDataManager<T> : IrowManager<T>
    {
        /// <summary>
        /// The connection that will be used by the manager.
        /// </summary>
        protected readonly MySqlConnection sqlConnection;

        /// <summary>
        /// Construct a new AbstractrowManager
        /// </summary>
        /// <param name="sqlConnection"></param>
        protected AbstractDataManager(MySqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Insert a row.
        /// </summary>
        /// <param name="obj">The row to insert.</param>
        /// <returns>If the operation was successful or not.</returns>
        public abstract bool Delete(T obj);

        /// <summary>
        /// Update a row.
        /// </summary>
        /// <param name="obj">The row to update.</param>
        /// <returns>If the operation was successful or not.</returns>
        public abstract bool Insert(T obj);

        /// <summary>
        /// Query a row.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The result of the query..</returns>
        public abstract T? Get(T obj);

        /// <summary>
        /// Delete a row.
        /// </summary>
        /// <param name="obj">The row to delete.</param>
        /// <returns>If the operation was successful or not.</returns>
        public abstract bool Update(T obj);
    }

    /// <summary>
    /// An interface that manages rows.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IrowManager<T>
    {
        /// <summary>
        /// Insert a row.
        /// </summary>
        /// <returns>If the operation was successful or not.</returns>
        bool Insert(T obj);

        /// <summary>
        /// Update a row.
        /// </summary>
        /// <returns>If the operation was successful or not.</returns>
        bool Update(T obj);

        /// <summary>
        /// Query a row.
        /// </summary>
        /// <returns>The result of the query.</returns>
        T? Get(T obj);

        /// <summary>
        /// Delete a row.
        /// </summary>
        /// <returns>If the operation was successful or not.</returns>
        bool Delete(T obj);
    }
}
