using Microsoft.AspNetCore.Mvc;

namespace lucentrp.Features.Users
{
    /// <summary>
    /// The controller responsible for all user endpoints.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The logger that will be used by the user controller.
        /// </summary>
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Construct a new user controller.
        /// </summary>
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
    }
}
