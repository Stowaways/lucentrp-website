using lucentrp.Features.Authentication;
using lucentrp.Shared.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace lucentrp.Features.Users
{
    /// <summary>
    /// The controller responsible for all user endpoints.
    /// </summary>
    [Authenticate]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The logger that will be used by the user controller.
        /// </summary>
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// The command that will be used to insert user accounts.
        /// </summary>
        private readonly IInsertUserAccount _insertUserAccount;

        /// <summary>
        /// The command that will be used to update user accounts.
        /// </summary>
        private readonly IUpdateUserAccount _updateUserAccount;

        /// <summary>
        /// The command that will be used to delete user accounts.
        /// </summary>
        private readonly IDeleteUserAccount _deleteUserAccount;

        /// <summary>
        /// The query that will be used to get users accounts by their id.
        /// </summary>
        private readonly IGetUserAccountByID _getUserAccountByID;

        /// <summary>
        /// Construct a new user controller.
        /// </summary>
        public UserController(
            ILogger<UserController> logger, 
            IInsertUserAccount insertUserAccount, 
            IUpdateUserAccount updateUserAccount, 
            IDeleteUserAccount deleteUserAccount, 
            IGetUserAccountByID getUserAccountByID
        )
        {
            _logger = logger;
            _insertUserAccount = insertUserAccount;
            _updateUserAccount = updateUserAccount;
            _deleteUserAccount = deleteUserAccount;
            _getUserAccountByID = getUserAccountByID;
        }
    }
}
