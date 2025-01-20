using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Application.Common.Models;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Commands.CreateProperty
{
    public record CreatePropertyCommand
    (
        string Name,
        AddressRequest Address,
        PriceRequest Price,
        string CodeInternal,
        int Year,
        Guid OwnerId
    ) : IRequest<ErrorOr<PropertyBuilding>>;


}