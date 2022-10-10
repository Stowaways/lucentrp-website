namespace LucentRP.Features.Authorization
{
    /// <summary>
    /// An attribute that can be used to require requesting users to have
    /// at least one of the permissions specified to use an endpoint or 
    /// controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AuthorizeAnyAttribute : Attribute
    {
        /// <summary>
        /// The permissions.
        /// </summary>
        public string[] Permissions { get; protected set; }

        /// <summary>
        /// Construct a new AuthorizateAny attribute.
        /// </summary>
        /// 
        /// <remarks>
        /// This attribute can only be used on endpoints that
        /// have the authentication attribute.
        /// </remarks>
        /// 
        /// <param name="permissions">The permission that a requesting
        /// user must have one of to access the endpoint or controller.</param>
        public AuthorizeAnyAttribute(string[] permissions)
        {
            Permissions = permissions;
        }
    }
}
