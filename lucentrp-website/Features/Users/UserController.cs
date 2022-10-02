using FluentValidation.Results;
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
        /// The token manager that will be used to encode and decode tokens.
        /// </summary>
        private readonly TokenManager _tokenManager;

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
        /// The query that will be used to get users accounts by their username.
        /// </summary>
        private readonly IGetUserAccountByUsername _getUserAccountByUsername;

        /// <summary>
        /// The validator that will be used to validate login requests.
        /// </summary>
        private readonly UserAccountLoginValidator _userAccountLoginValidator;

        /// <summary>
        /// Construct a new user controller.
        /// </summary>
        public UserController(
            ILogger<UserController> logger, 
            TokenManager tokenManager,
            IInsertUserAccount insertUserAccount, 
            IUpdateUserAccount updateUserAccount, 
            IDeleteUserAccount deleteUserAccount, 
            IGetUserAccountByID getUserAccountByID,
            IGetUserAccountByUsername getUserAccountByUsername,
            UserAccountLoginValidator userAccountLoginValidator
        )
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _insertUserAccount = insertUserAccount;
            _updateUserAccount = updateUserAccount;
            _deleteUserAccount = deleteUserAccount;
            _getUserAccountByID = getUserAccountByID;
            _getUserAccountByUsername = getUserAccountByUsername;
            _userAccountLoginValidator = userAccountLoginValidator;
        }

        [Anonymous]
        [HttpPost]
        public IActionResult Login(UserAccount userAccount)
        {
            try
            {
                // Validate the request.
                ValidationResult results = _userAccountLoginValidator.Validate(userAccount);
                
                // If the request is invalid.
                if (!results.IsValid)
                    return BadRequest(results.Errors);

                // Get the users information from the database.
                UserAccount? targetAccount = _getUserAccountByUsername.Execute(userAccount.Username);

                // If the user account could not be found.
                if (targetAccount == null)
                    return Unauthorized();

                // If the user provided the wrong password.
                if (!AuthUtilities.VerifyPassword(userAccount.Password, targetAccount.Password))
                    return Unauthorized();

                // Create the user's token.
                string token = _tokenManager.CreateToken(targetAccount);

                Response.Cookies.Append("Authorization", $"Bearer {token}", new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict });
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while a user was attempting to login: {Message}", ex.Message);
                return StatusCode(500);
            }
        }
    }
}
