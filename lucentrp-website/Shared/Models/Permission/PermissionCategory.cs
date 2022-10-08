namespace LucentRP.Shared.Models.Permission
{
    /// <summary>
    /// A class to model database permission categories.
    /// </summary>
    public class PermissionCategory
    {
        /// <summary>
        /// The id of the category.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Construct a permission category.
        /// </summary>
        /// <param name="id">The id of the category.</param>
        /// <param name="name">The name of the category.</param>
        public PermissionCategory(long id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
