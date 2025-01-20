using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Entities;
using RealState.Domain.Interfaces;

namespace RealState.Domain.Common.Models
{
    public abstract class Entity : IHasDomainEvents
    {
        public Guid Id { get; protected set; }
        private List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        IReadOnlyList<IDomainEvent> IHasDomainEvents.DomainEvents => throw new NotImplementedException();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }


    }
}