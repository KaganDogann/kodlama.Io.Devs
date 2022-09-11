using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Commands.RegisterUser
{
    public class RegisterCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(u => u.UserForRegisterDto.Email).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.Email).EmailAddress();
            RuleFor(u => u.UserForRegisterDto.Password).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.LastName).NotEmpty();
        }
    }
}
