using Ecommerce.Core.DTO;
using FluentValidation;

namespace Ecommerce.Core.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(temp => temp.Email).NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email adress format")
            ;
            RuleFor(temp => temp.Password).NotEmpty(); 
            
        }
    }
}