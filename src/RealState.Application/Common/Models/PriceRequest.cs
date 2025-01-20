using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Application.Common.Models
{
     public record PriceRequest
    (
        decimal Amount,
        string Currency
    );
}