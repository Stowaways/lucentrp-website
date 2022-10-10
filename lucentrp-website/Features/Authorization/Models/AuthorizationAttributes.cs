namespace LucentRP.Features.Authorization
{
    /// <summary>
    /// A modle to represent authorization attributes that belong
    /// to an endpoint and controller.
    /// </summary>
    public class AuthorizationAttributes
    {
        /// <summary>
        /// The endpoint's pair of attributes.
        /// </summary>
        public AuthorizationPair Endpoint { get; }

        /// <summary>
        /// The controller's pair of attributes.
        /// </summary>
        public AuthorizationPair Controller { get; }

        /// <summary>
        /// Construct a new AuthorizationAttributes model.
        /// </summary>
        /// 
        /// <param name="endpoint">The endpoint's pair of attributes.</param>
        /// <param name="controller">The controller's pair of attributes.</param>
        public AuthorizationAttributes(AuthorizationPair endpoint, AuthorizationPair controller)
        {
            Endpoint = endpoint;
            Controller = controller;
        }
    }

    /// <summary>
    /// A model to pair together authorize any and authorize all attributes.
    /// </summary>
    public class AuthorizationPair
    {
        /// <summary>
        /// The authorize any attribute.
        /// </summary>
        public AuthorizeAnyAttribute? AuthorizeAnyAttribute { get; }

        /// <summary>
        /// The authorize all attribute.
        /// </summary>
        public AuthorizeAllAttribute? AuthorizeAllAttribute { get;}

        /// <summary>
        /// Construct a new AuthorizationPair.
        /// </summary>
        /// 
        /// <param name="authorizeAnyAttribute">The authorize any attribute.</param>
        /// <param name="authorizeAllAttribute">The authorize all attribute.</param>
        public AuthorizationPair(AuthorizeAnyAttribute? authorizeAnyAttribute, AuthorizeAllAttribute? authorizeAllAttribute)
        {
            AuthorizeAnyAttribute = authorizeAnyAttribute;
            AuthorizeAllAttribute = authorizeAllAttribute;
        }
    }
}
