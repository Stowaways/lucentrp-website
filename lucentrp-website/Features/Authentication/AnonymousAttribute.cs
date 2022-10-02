namespace lucentrp.Features.Authentication
{
    /// <summary>
    /// An attribute that can be used to allow anonoymous requests on endpoints and controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AnonymousAttribute : Attribute
    {
    }
}
