using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.DTO;
using FluentValidation;

namespace Ecommerce.Core.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(temp => temp.PersonName).NotEmpty().WithMessage("Person Name must not be empty").Length(1, 30).WithMessage("Name must be from 1 to 30 characters long");
            RuleFor(temp => temp.Email).NotEmpty().WithMessage("Email adress is required")
            .EmailAddress().WithMessage("Invalid email adress");
            RuleFor(temp => temp.Password).NotEmpty().WithMessage("Passoword must not be empty");
            RuleFor(temp => temp.Gender).IsInEnum().WithMessage("Gender option must be one of the following");
            
        }
    }
}