using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Domain.Entities;


namespace RealState.Application.Owners.CreateOwner
{
    public record CreateOwnerCommand
    (
        string Name,
        Address Address,
        string? Photo,
        DateTime Birthday   

    ): IRequest<ErrorOr<Owner>>;
        
    public record Address
    (
        string Street,
        string City,
        string ZipCode 
    );
}