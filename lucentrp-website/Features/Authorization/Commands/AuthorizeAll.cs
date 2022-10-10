using LucentRP.Features.Permissions;
using LucentRP.Shared.Models.Permission;
using LucentRP.Shared.Models.User;

namespace LucentRP.Features.Authorization
{
    /// <summary>
    /// A command to authorize users that contain all
    /// of the permissions in a collection of permissions.
    /// </summary>
    public class AuthorizeAll : IAuthorizeAll
    {
        /// <summary>
        /// The permission assignment manager that will be used to
        /// get the permissions that have been assigned to users.
        /// </summary>
        private readonly PermissionAssignmentManager _permissionAssignmentManager;

        /// <summary>
        /// Construct a new AuthorizeAll command.
        /// </summary>
        /// 
        /// <param name="permissionAssignmentManager">The permission assignment
        /// manager that will be used to get the permissions that have been 
        /// assigned to users.</param>
        public AuthorizeAll(PermissionAssignmentManager permissionAssignmentManager)
        {
            _permissionAssignmentManager = permissionAssignmentManager;
        }

        /// <summary>
        /// Execute the authorization command.
        /// </summary>
        /// 
        /// <param name="userAccount">The user account that is being authorized.</param>
        /// <param name="permissions">The collection of permissions that the user
        /// must have to be authorized.</param>
        /// <returns>If the user is authorized or not.</returns>
        public bool Execute(UserAccount userAccount, string[] permissions)
        {
            IEnumerable<PermissionNode> permissionNodes = _permissionAssignmentManager.GetUserPermissionNodes(userAccount);

            return permissions.All(permission => permissionNodes.Any(node => node.Name.Equals(permission)));
        }
    }

    /// <summary>
    /// A command to authorize users that contain all
    /// of the permissions in a collection of permissions.
    /// </summary>
    public interface IAuthorizeAll
    {
        /// <summary>
        /// Execute the authorization command.
        /// </summary>
        /// 
        /// <param name="userAccount">The user account that is being authorized.</param>
        /// <param name="permissions">The collection of permissions that the user
        /// must have to be authorized.</param>
        /// <returns>If the user is authorized or not.</returns>
        bool Execute(UserAccount userAccount, string[] permissions);
    }
}
