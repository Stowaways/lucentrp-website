namespace LucentRP.Shared.Models.Permission
{
    /// <summary>
    /// A class to model database permissions.
    /// </summary>
    public class PermissionNode
    {
        /// <summary>
        /// The permission's id.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// The category that the permission belongs to.
        /// </summary>
        public long CategoryID { get; set; }

        /// <summary>
        /// The name of the permission.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Construct a new permission.
        /// </summary>
        /// <param name="id">The permission's id.</param>
        /// <param name="categoryID">The id of the category the permission belongs to.</param>
        /// <param name="name">The permission's name.</param>
        public PermissionNode(long id, long categoryID, string name)
        {
            ID = id;
            CategoryID = categoryID;
            Name = name;
        }
    }
}
