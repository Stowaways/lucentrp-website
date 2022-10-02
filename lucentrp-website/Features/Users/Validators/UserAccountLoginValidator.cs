using FluentValidation;
using lucentrp.Shared.Models.User;
using lucentrp.Shared.Validator;

namespace lucentrp.Features.Users
{
    /// <summary>
    /// A validator that can
    /// </summary>
    public class UserAccountLoginValidator : ModelValidator<UserAccount>
    {
        public UserAccountLoginValidator(IConfigurationRoot config, string basePath = "Validation:User:") : base(config, basePath)
        {
            RuleFor(user => user.Username).NotEmpty().NotNull().WithMessage("Username is not defined");
            RuleFor(user => user.Password).NotEmpty().NotNull().WithMessage("Password is not defined");
        }
    }
}
