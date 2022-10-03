using FluentValidation;
using lucentrp.Shared.Models.User;
using lucentrp.Shared.Validator;

namespace lucentrp.Features.Users
{
    /// <summary>
    /// A validator that can be used to validate login requests.
    /// </summary>
    public class UserAccountLoginValidator : ModelValidator<UserAccount>
    {
        /// <summary>
        /// Construct a new UserAccountLoginValidator.
        /// </summary>
        /// <param name="config">The configuration file containing user account validation parameters.</param>
        /// <param name="basePath">The base path of the configuration section.</param>
        public UserAccountLoginValidator(IConfigurationRoot config, string basePath = "Validation:User:") : base(config, basePath)
        {
            RuleFor(user => user.Username).NotEmpty().NotNull().WithMessage("Username is not defined");
            RuleFor(user => user.Password).NotEmpty().NotNull().WithMessage("Password is not defined");
        }
    }
}
