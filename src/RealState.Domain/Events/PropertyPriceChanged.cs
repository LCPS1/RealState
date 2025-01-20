using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Interfaces;
using RealState.Domain.ValueObjects;

namespace RealState.Domain.Events
{   public record PropertyPriceChanged(
        Guid PropertyId,
        Price OldPrice,
        Price NewPrice
    ) : IDomainEvent;
}