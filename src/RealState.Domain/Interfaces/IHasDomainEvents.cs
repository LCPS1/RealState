using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Interfaces;

namespace RealState.Domain.Entities
{
    public interface IHasDomainEvents
    {
        public IReadOnlyList<IDomainEvent> DomainEvents {get;}

        public void ClearDomainEvents();
    }
}