using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Entities;
using RealState.Domain.Interfaces;

namespace RealState.Domain.Events
{
     public class PropertyTraceAdded : IDomainEvent
    {
        public PropertyBuilding PropertyBuilding { get; }
        public PropertyTrace PropertyTrace { get; }

        public PropertyTraceAdded(PropertyBuilding propertyBuilding, PropertyTrace propertyTrace)
        {
            PropertyBuilding = propertyBuilding;
            PropertyTrace = propertyTrace;
        }
    }
}