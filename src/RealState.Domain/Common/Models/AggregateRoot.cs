using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.ValueObjects;
using RealState.Domain.Common.Models;
using RealState.Domain.Interfaces;

namespace RealState.Domain.Common.Models
{
    public abstract class AggregateRoot: Entity
    {
        public int Version { get; protected set; }
        protected AggregateRoot()
        {
            //_domainEvents = new List<IDomainEvent>();
        }

    }
}