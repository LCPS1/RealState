using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Commands.PatchPriceProperty
{
    public record PatchPricePropertyCommand
   ( 
        Guid Id ,
        Price Price
   ): IRequest<ErrorOr<PropertyBuilding>>;

     public record Price
    (
        decimal Amount,
        string Currency
    );
}