using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Common.Models;
using RealState.Domain.ValueObjects;

namespace RealState.Domain.Entities
{
   public class PropertyTrace : Entity
    {
        public DateTime DateSale { get; private set; }
        public string Name { get; private set; }
        public decimal Value { get; private set; }

        public Tax Tax { get; private set; }
        public Guid PropertyId { get; private set; }

        // Navigation property
        public virtual PropertyBuilding PropertyBuilding { get; private set; }
        private PropertyTrace()
        {
        }

        public PropertyTrace(DateTime dateSale, string name, decimal value, Guid propertyId)
        {
            //Id = id;
            DateSale = dateSale;
            Name = name;
            Value = value;
            PropertyId = propertyId;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            Name = name;
        }

        public void ChangeValue(decimal value)
        {
            Value = value;
        }
    }
}