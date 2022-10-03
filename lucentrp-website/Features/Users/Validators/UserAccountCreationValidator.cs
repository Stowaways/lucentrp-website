using FluentValidation;
using lucentrp.Shared.Models.User;
using lucentrp.Shared.Validator;

namespace lucentrp.Features.Users
{
    /// <summary>
    /// A validator that can be used to validate user account creation requests.
    /// </summary>
    public class UserAccountCreationValidator : ModelValidator<UserAccount>
    {
        /// <summary>
        /// The query that will be used to determine if usernames are unique.
        /// </summary>
        private readonly IGetUserAccountByUsername _getUserAccountByUsername;

        /// <summary>
        ///  The query that will be used to determin if email addresses are unique.
        /// </summary>
        private readonly IGetUserAccountByEmail _getUserAccountByEmail;

        /// <summary>
        /// Construct a new UserAccountCreationValidator.
        /// </summary>
        /// 
        /// <remarks>
        /// Validation may throw an exception if there is an issue querying the database.
        /// </remarks>
        /// 
        /// <param name="config">The configuration file containing user account validation parameters.</param>
        /// <param name="basePath">The base path of the configuration section.</param>
        public UserAccountCreationValidator(
            IConfigurationRoot config, 
            IGetUserAccountByUsername getUserAccountByUsername,
            IGetUserAccountByEmail getUserAccountByEmail,
            string basePath = "Validation:User:CreateAccount:"
        ) : base(config, basePath)
        {
            _getUserAccountByUsername = getUserAccountByUsername;
            _getUserAccountByEmail = getUserAccountByEmail;

            // Email validation rules.
            if (GetBool("Email:Required"))
                RuleFor(user => user.Email)
                    .NotNull()
                        .WithMessage("Email is required");

            RuleFor(user => user.Email)
                .MinimumLength(GetInt("Email:MinimumLength"))
                    .WithMessage($"Email must be at least {GetInt("Email:MinimumLength")} characters long")
                .MaximumLength(GetInt("Email:MaximumLength"))
                    .WithMessage($"Email must not exceed {GetInt("Email:MinimumLength")} characters")
                .EmailAddress()
                    .WithMessage("Invalid email address");

            // Username validation rules.
            if (GetBool("Username:Required"))
                RuleFor(user => user.Email)
                    .NotNull()
                        .WithMessage("Username is required");

            RuleFor(user => user.Username)
                .MinimumLength(GetInt("Username:MinimumLength"))
                    .WithMessage($"Username must be at least {GetInt("Username:MinimumLength")} characters long")
                .MaximumLength(GetInt("Username:MaximumLength"))
                    .WithMessage($"Username must not exceed {GetInt("Username:MinimumLength")} characters");

            // Password validation rules.
            RuleFor(user => user.Password)
            .MinimumLength(GetInt("Password:MinimumLength"))
                .WithMessage($"Password must be at least {GetInt("Password:MinimumLength")} characters long")
            .MaximumLength(GetInt("Username:MaximumLength"))
                .WithMessage($"Password must not exceed {GetInt("Password:MinimumLength")} characters");

            // Uniqueness queries (keep them at the end as they take a lot of time to execute).
            if (GetBool("Email:Unique"))
                RuleFor(user => user.Email)
                    .Must(email => _getUserAccountByEmail.Execute(email) == null)
                        .WithMessage("Email address already in use");

            if (GetBool("Username:Unique"))
                RuleFor(user => user.Username)
                    .Must(username => _getUserAccountByUsername.Execute(username) == null)
                        .WithMessage("Username address already in use");
        }
    }
}
