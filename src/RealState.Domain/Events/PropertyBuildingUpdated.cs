using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Entities;
using RealState.Domain.Interfaces;

namespace RealState.Domain.Events
{
    public class PropertyBuildingUpdated : IDomainEvent
    {
        public PropertyBuilding PropertyBuilding { get; }

        public PropertyBuildingUpdated(PropertyBuilding propertyBuilding)
        {
             PropertyBuilding = propertyBuilding;  
        }
    }
}