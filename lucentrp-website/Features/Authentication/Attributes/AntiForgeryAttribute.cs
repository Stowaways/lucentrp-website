namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// An attribute that can be used to prevent cross-site 
    /// request forgery on endpoints and controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AntiForgeryAttribute : Attribute
    {
    }
}
