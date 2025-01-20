using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.Common.DTOs
{
    public record PropertyDTO
    (
        Guid Id,
        string Name,
        AddressDTO Address,
        PriceDTO Price,
        string CodeInternal,
        int Year,
        Guid OwnerId
    );
}