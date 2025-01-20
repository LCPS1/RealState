using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Commands.PatchPriceProperty
{
    public class PatchPricePropertyCommandValidator : AbstractValidator<PropertyBuilding>
    {
        public PatchPricePropertyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}