﻿using FluentValidation.Results;
using LucentRP.Features.Authentication;
using LucentRP.Shared.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace LucentRP.Features.Users
{
    /// <summary>
    /// The controller responsible for all user endpoints.
    /// </summary>
    [Authenticate]
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
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
        /// The validator that will be used to validate account login requests.
        /// </summary>
        private readonly UserAccountLoginValidator _userAccountLoginValidator;

        /// <summary>
        /// The validator that will be used to validate account creation requests.
        /// </summary>
        private readonly UserAccountCreationValidator _userAccountCreationValidator;

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
            UserAccountLoginValidator userAccountLoginValidator,
            UserAccountCreationValidator userAccountCreationValidator
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
            _userAccountCreationValidator = userAccountCreationValidator;
        }

        /// <summary>
        /// Create a user account.
        /// </summary>
        /// <param name="userAccount">The user account's informaion.</param>
        /// <returns>If the request was successful or not.</returns>
        [Anonymous]
        [HttpPost]
        public IActionResult Create(UserAccount userAccount)
        {
            try
            {
                // Validate the request.
                ValidationResult result = _userAccountCreationValidator.Validate(userAccount);

                // If the request is invalid.
                if (!result.IsValid)
                    return BadRequest(result.Errors);

                // Hash the user's password.
                userAccount.Password = AuthUtilities.HashPassword(userAccount.Password);

                // Insert the user's account.
                bool success = _insertUserAccount.Execute(userAccount);

                // Return the result of the insertion.
                return success ? Ok() : StatusCode(500);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while a user was attempting to create an account: {Message}", ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Login to a user account.
        /// </summary>
        /// <param name="userAccount">The user account to login to.</param>
        /// <returns>An authentication cookie if the credentials were correct, 
        /// otherwise an error code.</returns>
        [Anonymous]
        [HttpPost]
        public IActionResult Login(UserAccount userAccount)
        {
            try
            {
                // Validate the request.
                ValidationResult result = _userAccountLoginValidator.Validate(userAccount);
                
                // If the request is invalid.
                if (!result.IsValid)
                    return BadRequest(result.Errors);

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

        [Authenticate]
        [HttpPost]
        /// <summary>
        /// Log out of a user account.
        /// </summary>
        /// <returns>A cookie to log the requester out.</returns>
        public IActionResult Logout()
        {
            Response.Cookies.Append("Authorization", $"", new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict });
            return Ok();
        }

        /// <summary>
        /// Check if the request contains a valid authentication cookie.
        /// </summary>
        /// <returns>If the sender is authenticated or not.</returns>
        [Anonymous]
        [HttpPost]
        public IActionResult CheckAuthentication()
        {
            // Get the authentication cookie.
            string? authCookie = Request.Cookies["Authorization"];

            // If the authentication cookie was not found.
            if (string.IsNullOrEmpty(authCookie))
                return Ok(false);

            // If the cookie is malformed.
            if (!authCookie.StartsWith("Bearer "))
                return Ok(false);

            // Decode the token.
            IDictionary<string, object> claims;

            try
            {
                claims = _tokenManager.DecodeToken(authCookie.Replace("Bearer ", ""));
            }
            catch
            {
                return Ok(false);
            }

            // If the token does not provide the required claims.
            if (claims["id"] == null || claims["password"] == null)
                return Ok(false);

            // If the claims are provided as the wrong datatype.
            if (claims["id"].GetType() != typeof(long) || claims["password"].GetType() != typeof(string))
                return Ok(false);

            // Load the user's account.
            UserAccount? userAccount;

            try
            {
                userAccount = _getUserAccountByID.Execute((long)claims["id"]);
            }
            catch
            {
                return StatusCode(500);
            }

            // If the user's account could not be found.
            if (userAccount == null)
                return Ok(false);

            // If the passwords do not match.
            if (!userAccount.Password.Equals(claims["password"]))
                return Ok(false);

            // The user is authenticated.
            return Ok(true);
        }
    }
}
