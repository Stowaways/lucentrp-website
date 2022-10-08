namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// An attribute that can be used to allow anonoymous requests on endpoints and controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AnonymousAttribute : Attribute
    {
    }
}
