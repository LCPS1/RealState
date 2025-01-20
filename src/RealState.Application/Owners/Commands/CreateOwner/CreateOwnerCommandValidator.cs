using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RealState.Domain.Entities;

namespace RealState.Application.Owners.CreateOwner
{
    
    public class CreateOwnerCommandValidator : AbstractValidator<Owner>
    {
        public CreateOwnerCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}