using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using MediatR;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Queries.GetProperties
{
    public class GetPropertiesQueryValidator : AbstractValidator<PropertyBuilding>
    {
        public GetPropertiesQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}