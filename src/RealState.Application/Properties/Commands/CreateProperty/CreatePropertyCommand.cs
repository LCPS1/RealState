using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Commands.CreateProperty
{
    public record CreatePropertyCommand
    (
        string Name,
        Address Address,
        Price Price,
        string CodeInternal,
        int Year,
        Guid OwnerId
    ) : IRequest<ErrorOr<PropertyBuilding>>;


    public record Address
    (
        string Street,
        string City ,
        string ZipCode 
    );

    public record Price
    (
        decimal Amount,
        string Currency
    );
}