using LucentRP.Features.Permissions;
using LucentRP.Shared.Models.Permission;
using LucentRP.Shared.Models.User;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace LucentRP.Features.Authorization
{
    /// <summary>
    /// Middleware that will perform authorization.
    /// </summary>
    public class AuthorizationMiddleware
    {
        /// <summary>
        /// The permission assignment manager that will be used to
        /// get the permissions that have been assigned to users.
        /// </summary>
        private readonly PermissionAssignmentManager _permissionAssignmentManager;

        /// <summary>
        /// Construct AuthorizeMiddleware.
        /// </summary>
        /// 
        /// <param name="permissionAssignmentManager">The permission assignment manager 
        /// that will be used to get the permissions that have been assigned to users.
        /// </param>
        public AuthorizationMiddleware(PermissionAssignmentManager permissionAssignmentManager)
        {
            _permissionAssignmentManager = permissionAssignmentManager;
        }

        /// <summary>
        /// Execute the authorization.
        /// </summary>
        /// 
        /// <remarks>
        /// This middleware should only be used AFTER authentication has been
        /// performed.
        /// </remarks>
        /// 
        /// <param name="context">The request context.</param>
        /// <param name="next">The next middlware function to call.</param>
        /// <returns>A task.</returns>
        public async Task Execute(HttpContext context, Func<Task> next)
        {
            // Get the authorization attributes of the request.
            AuthorizationAttributes authorizationAttributes = GetAuthorizationAttributes(context);

            // If authorization is not required.
            if (!AuthorizationIsRequired(authorizationAttributes))
            {
                await next();
                return;
            }

            // Get the user account (if this is null, they have not been authenticated).
            UserAccount? userAccount = (UserAccount?)context.Items["account"];

            // If the user has not been authenticated.
            if (userAccount is null)
            {
                context.Response.StatusCode = 401;
                await context.Response.CompleteAsync();
                return;
            }

            // Get the user's permission nodes.
            bool isAuthorized = HasRequiredPermissions(authorizationAttributes, GetPermissionNodes(userAccount));

            // If the user is not authorized.
            if (!isAuthorized)
            {
                context.Response.StatusCode = 403;
                await context.Response.CompleteAsync();
                return;
            }

            // The user has been authorized, call the next middleware.
            await next();
        }

        /// <summary>
        /// Get the permission nodes that have been assigned to a user.
        /// </summary>
        /// 
        /// <param name="userAccount">The user whose permission nodes are being retrieved.</param>
        /// <returns>The user's permission nodes.</returns>
        private string[] GetPermissionNodes(UserAccount userAccount)
        {
            // Get the user's permission nodes.
            IEnumerable<PermissionNode> permissionNodes = _permissionAssignmentManager.GetUserPermissionNodes(userAccount);
            return (
                from PermissionNode node
                in permissionNodes
                select node.Name
            ).ToArray();
        }

        /// <summary>
        /// Get the authorization attributes of the request's endpoint and controller.
        /// </summary>
        /// 
        /// <param name="context">The request's context.</param>
        /// <returns>The authorization attributes.</returns>
        private static AuthorizationAttributes GetAuthorizationAttributes(HttpContext context)
        {
            // Get the endpoint that is being requested.
            Endpoint? endpoint = context.GetEndpoint();

            // If the endpoint does not exist.
            if (endpoint is null)
                return new AuthorizationAttributes(
                    new AuthorizationPair(null, null),
                    new AuthorizationPair(null, null)
                );

            // Get the endpoint's authentication attributes.
            AuthorizeAnyAttribute? endpointAuthorizeAnyAttribute = (AuthorizeAnyAttribute?)endpoint.Metadata.FirstOrDefault(m => m is AuthorizeAnyAttribute);
            AuthorizeAllAttribute? endpointAuthorizeAllAttribute = (AuthorizeAllAttribute?)endpoint.Metadata.FirstOrDefault(m => m is AuthorizeAllAttribute);

            // Get the controlle that the endpoint belongs to (may not belong to one).
            ControllerActionDescriptor? controller = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            AuthorizeAnyAttribute? controllerAuthorizeAnyAttribute = null;
            AuthorizeAllAttribute? controllerAuthorizeAllAttribute = null;

            // If the endpoint belongs to a controller.
            if (controller is not null)
            {
                controllerAuthorizeAnyAttribute = (AuthorizeAnyAttribute?)controller.ControllerTypeInfo.GetCustomAttributes(typeof(AuthorizeAnyAttribute), true).FirstOrDefault();
                controllerAuthorizeAllAttribute = (AuthorizeAllAttribute?)controller.ControllerTypeInfo.GetCustomAttributes(typeof(AuthorizeAllAttribute), true).FirstOrDefault();
            }

            return new AuthorizationAttributes(
                new AuthorizationPair(endpointAuthorizeAnyAttribute, endpointAuthorizeAllAttribute),
                new AuthorizationPair(controllerAuthorizeAnyAttribute, controllerAuthorizeAllAttribute)
            );
        }

        /// <summary>
        /// Check if authorization is required based on the attributes.
        /// </summary>
        /// 
        /// <param name="attributes">The attributes.</param>
        /// <returns>If authorization is required or not.</returns>
        private static bool AuthorizationIsRequired(AuthorizationAttributes attributes)
        {
            return attributes.Endpoint.AuthorizeAnyAttribute is not null   ||
                   attributes.Endpoint.AuthorizeAllAttribute is not null   ||
                   attributes.Controller.AuthorizeAnyAttribute is not null ||
                   attributes.Controller.AuthorizeAllAttribute is not null;
        }

        /// <summary>
        /// Check if the user has any of the attribute's permissions.
        /// </summary>
        /// 
        /// <param name="userPermissions">The user's permissions.</param>
        /// <param name="attributePermissions">The attribute's permissions.</param>
        /// <returns>If the user has any of the attribute's permissions.</returns>
        private static bool HasAnyPermissions(string[] userPermissions, string[] attributePermissions)
        {
            return attributePermissions.Any(attributePermission => userPermissions.Contains(attributePermission));
        }

        /// <summary>
        /// Check if the user has all of the attribute's permissions.
        /// </summary>
        /// 
        /// <param name="userPermissions">The user's permissions.</param>
        /// <param name="attributePermissions">The attribute's permissions.</param>
        /// <returns>If the user has all of the attribute's permissions.</returns>
        private static bool HasAllPermissions(string[] userPermissions, string[] attributePermissions)
        {
            return attributePermissions.All(attributePermission => userPermissions.Contains(attributePermission));
        }

        /// <summary>
        /// Check if a colletion of permissions has the required permissions based on the authorization attributes.
        /// </summary>
        /// 
        /// <param name="authorizationAttributes">The authorization attributes.</param>
        /// <param name="permissions">The permissions.</param>
        /// <returns></returns>
        private static bool HasRequiredPermissions(AuthorizationAttributes authorizationAttributes, string[] permissions) {
            bool isAuthorized = true;

            // If the endpoint requires the user to have any permission in a collection of permissions.
            if (authorizationAttributes.Endpoint.AuthorizeAnyAttribute is not null)
                isAuthorized = isAuthorized && HasAnyPermissions(permissions, authorizationAttributes.Endpoint.AuthorizeAnyAttribute.Permissions);

            // If the endpoint requires the user to have all permissions in a collection of permissions.
            if (authorizationAttributes.Endpoint.AuthorizeAllAttribute is not null)
                isAuthorized = isAuthorized && HasAllPermissions(permissions, authorizationAttributes.Endpoint.AuthorizeAllAttribute.Permissions);

            // If the controller requires the user to have any permission in a collection of permissions.
            if (authorizationAttributes.Controller.AuthorizeAnyAttribute is not null)
                isAuthorized = isAuthorized && HasAnyPermissions(permissions, authorizationAttributes.Controller.AuthorizeAnyAttribute.Permissions);

            // If the controller requires the user to have all permissions in a collection of permissions.
            if (authorizationAttributes.Controller.AuthorizeAllAttribute is not null)
                isAuthorized = isAuthorized && HasAllPermissions(permissions, authorizationAttributes.Controller.AuthorizeAllAttribute.Permissions);

            return isAuthorized;
        }
    }
}
