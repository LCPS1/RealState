using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Contracts.Common.DTOs;

namespace RealState.Contracts.Properties.CreateProperty
{
    public record CreatePropertyRequest
    (
        string Name,
        AddressDTO Address,
        PriceDTO Price,
        string CodeInternal,
        int Year,
        Guid OwnerId
    );
    
    
}