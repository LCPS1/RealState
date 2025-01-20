using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.Owners.CreateOwner
{
    public record CreateOwnerResponse
    (
        Guid Id ,
        string Name,
        CreateOwnerAddressResponse Address,
        DateTime Birthday 
    );

    public record CreateOwnerAddressResponse
    (
        string Street,
        string City,
        string ZipCode
    );
}