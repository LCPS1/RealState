using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace RealState.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);
             RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);
             RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(50);
             RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}