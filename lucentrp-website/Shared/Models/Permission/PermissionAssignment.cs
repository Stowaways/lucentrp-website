namespace LucentRP.Shared.Models.Permission
{
    /// <summary>
    /// A class to model database permission assignments.
    /// </summary>
    public class PermissionAssignment
    {
        /// <summary>
        /// The permission assignment's id.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// The id of the user the permission is assigned to.
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// The id of the permission that has been assigned.
        /// </summary>
        public long PermissionID { get; set; }

        /// <summary>
        /// Construct a new PermissionAssignment.
        /// </summary>
        /// <param name="id">The id of the permission assignment.</param>
        /// <param name="userID">The id of the user account that the permission is being assigned to.</param>
        /// <param name="permissionID">The id of the permission that is being assigned.</param>
        public PermissionAssignment(long id, long userID, long permissionID)
        {
            ID = id;
            UserID = userID;
            PermissionID = permissionID;
        }
    }
}
