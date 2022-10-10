using LucentRP.Features.Permissions;
using LucentRP.Shared.Models.Permission;
using LucentRP.Shared.Models.User;

namespace LucentRP.Features.Authorization
{
    /// <summary>
    /// A command to authorize users that have any
    /// of the permissions in a collection of permissions.
    /// </summary>
    public class AuthorizeAny : IAuthorizeAny
    {
        /// <summary>
        /// The permission assignment manager that will be used to
        /// get the permissions that have been assigned to users.
        /// </summary>
        private readonly PermissionAssignmentManager _permissionAssignmentManager;

        /// <summary>
        /// Construct a new AuthorizeAny command.
        /// </summary>
        /// 
        /// <param name="permissionAssignmentManager">The permission assignment
        /// manager that will be used to get the permissions that have been 
        /// assigned to users.</param>
        public AuthorizeAny(PermissionAssignmentManager permissionAssignmentManager)
        {
            _permissionAssignmentManager = permissionAssignmentManager;
        }

        /// <summary>
        /// Execute the authorization command.
        /// </summary>
        /// 
        /// <param name="userAccount">The user account that is being authorized.</param>
        /// <param name="permissions">The collection of permissions that the user
        /// must have one to be authorized.</param>
        /// <returns>If the user is authorized or not.</returns>
        public bool Execute(UserAccount userAccount, string[] permissions)
        {
            // Get the permission nodes that the user has.
            IEnumerable<PermissionNode> permissionNodes = _permissionAssignmentManager.GetUserPermissionNodes(userAccount);

            return permissions.Any(permission => permissionNodes.Any(node => node.Name.Equals(permission)));
        }
    }

    /// <summary>
    /// A command to authorize users that have any
    /// of the permissions in a collection of permissions.
    /// </summary>
    public interface IAuthorizeAny
    {
        /// <summary>
        /// Execute the authorization command.
        /// </summary>
        /// 
        /// <param name="userAccount">The user account that is being authorized.</param>
        /// <param name="permissions">The collection of permissions that the user
        /// must have one to be authorized.</param>
        /// <returns>If the user is authorized or not.</returns>
        bool Execute(UserAccount userAccount, string[] permissions);
    }
}
