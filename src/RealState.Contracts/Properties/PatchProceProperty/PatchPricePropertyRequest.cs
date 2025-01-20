using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.Properties.PatchProceProperty
{
    public record PatchPricePropertyRequest
    (
        Guid Id,
        PricePropertyRequest Price
    );

    public record PricePropertyRequest
    (
        Decimal Amount,
        string Currency    
    );
}