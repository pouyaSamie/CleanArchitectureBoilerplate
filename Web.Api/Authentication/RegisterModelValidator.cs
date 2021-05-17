using FluentValidation;
using System.Text.RegularExpressions;

namespace Web.Api.Authentication
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(5, 50);

            RuleFor(x => x.MobileNumber).Must(BePhoneNumber).WithMessage("شماره موبایل صحیح نمی باشد.");

            RuleFor(x => x.Password).Equal(x => x.RepeatPassword);
        }

        private bool BePhoneNumber(string phoneNumber)
        {
            var mobileCheckPattern = "^(?:(\u0660\u0669[\u0660-\u0669][\u0660-\u0669]{8})|(\u06F0\u06F9[\u06F0-\u06F9][\u06F0-\u06F9]{8})|(09[0-9][0-9]{8}))$";
            return Regex.IsMatch(phoneNumber, mobileCheckPattern);
        }
    }
}
