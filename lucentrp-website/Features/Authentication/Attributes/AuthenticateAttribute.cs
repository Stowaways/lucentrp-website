namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// An attribute that can be used to require authentication on requests to endpoints and controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthenticateAttribute : Attribute
    {
    }
}
