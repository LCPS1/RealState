using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.Owners.CreateOwner
{
    public record CreateOwnerRequest
    (
        string Name,
        CreateOwnerAddressRequest Address,
        string? Photo,
        DateTime Birthday          
    );

    public record CreateOwnerAddressRequest
    (
        string Street,
        string City,
        string ZipCode
    );

}

