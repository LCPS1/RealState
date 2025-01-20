using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Commands.CreateProperty
{
    public class CreatePropertyValidator : AbstractValidator<PropertyBuilding>
    {
        public CreatePropertyValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.CodeInternal).NotEmpty();
            RuleFor(x => x.Year).NotEmpty();
            RuleFor(x => x.OwnerId).NotEmpty();
        }
    }
}