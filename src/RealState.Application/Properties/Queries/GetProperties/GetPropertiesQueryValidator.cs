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
    public class GetPropertiesQueryValidator : AbstractValidator<GetPropertiesQuery>
    {
         public GetPropertiesQueryValidator()
        {
             RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100)
            .WithMessage("PageSize must be between 1 and 100");

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxPrice.HasValue)
            .WithMessage("MaxPrice must be greater than or equal to 0");

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinPrice.HasValue)
            .WithMessage("MinPrice must be greater than or equal to 0");

        RuleFor(x => x.Year)
            .GreaterThanOrEqualTo(1800)
            .LessThanOrEqualTo(DateTime.UtcNow.Year)
            .When(x => x.Year.HasValue)
            .WithMessage($"Year must be between 1800 and {DateTime.UtcNow.Year}");

        RuleFor(x => x)
            .Must(x => x.MaxPrice == null || x.MinPrice == null || x.MaxPrice >= x.MinPrice)
            .When(x => x.MaxPrice.HasValue && x.MinPrice.HasValue)
            .WithMessage("MaxPrice must be greater than or equal to MinPrice");
        }
    }
}