using LucentRP.Features.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LucentRP.Features.Permissions
{
    /// <summary>
    /// The controller responsible for all permission endpoints.
    /// </summary>
    [Authenticate]
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class PermissionController : ControllerBase
    {
        /// <summary>
        /// The logger that will be used by the permission controller.
        /// </summary>
        private readonly ILogger<PermissionController> _logger;

        /// <summary>
        /// Construct a new permission controller.
        /// </summary>
        public PermissionController(ILogger<PermissionController> logger)
        {
            _logger = logger;
        }
    }
}
