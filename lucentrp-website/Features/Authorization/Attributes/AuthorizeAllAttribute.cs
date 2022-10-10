namespace LucentRP.Features.Authorization
{
    /// <summary>
    /// An attribute that can be used to require requesting users to have
    /// all of the permissions specified to use an endpoint or controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AuthorizeAllAttribute : Attribute
    {
        /// <summary>
        /// The permissions.
        /// </summary>
        public string[] Permissions { get; protected set; }

        /// <summary>
        /// Construct a new AuthorizeAll attribute.
        /// </summary>
        /// 
        /// <remarks>
        /// This attribute can only be used on endpoints that
        /// have the authentication attribute.
        /// </remarks>
        /// 
        /// <param name="permissions">The permissions that the requesting
        /// user must have to access the endpoint or controller.</param>
        public AuthorizeAllAttribute(string[] permissions)
        {
            Permissions = permissions;
        }
    }
}
