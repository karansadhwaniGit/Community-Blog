using CommunityBlog.Models.User;
using FluentValidation;

namespace CommunityBlog.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                  .Length(4, 10)
                  .WithMessage("Length Should be between 4 to 10 characters")
                  .NotEmpty()
                  .WithMessage("FirstName Cannot be Empty");

            RuleFor(x => x.LastName)
                .Length(4, 10)
                .WithMessage("Length Should be between 4 to 10 characters")
                .NotEmpty()
                .WithMessage("LastName Cannot be Empty");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid Email! It Cannot be Empty")
                .NotEmpty()
                .WithMessage("Email Cannot be Empty");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please Select Your Gender");

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Please Select Country");

            RuleFor(x => x.Phone)
                 .NotEmpty()
                 .WithMessage("Phone Cannot be Empty")
                 .Length(10)
                 .WithMessage("Please Write Correct 10 digit Contact Number");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username Cannot be Empty")
                .Length(6, 15)
                .WithMessage("UserName Should be between 6 to 15 letters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password Cannot be Empty")
                .MinimumLength(8)
                .WithMessage("Password Should be minimum 8 characters");

        }
    }
}
