using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Entities;
using RealState.Domain.Interfaces;

namespace RealState.Domain.Events
{
    public class PropertyBuildingCreated : IDomainEvent
    {
        public PropertyBuilding PropertyBuilding { get; }

        public PropertyBuildingCreated(PropertyBuilding propertyBuilding)
        {
            PropertyBuilding = propertyBuilding;
        }
    }
}