using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Application.Common.Models;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Commands.PatchPriceProperty
{
    public record PatchPricePropertyCommand
   ( 
        Guid Id ,
        PriceRequest Price
   ): IRequest<ErrorOr<PropertyBuilding>>;

}