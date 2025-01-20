using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Common.Models;
using RealState.Domain.Events;
using RealState.Domain.ValueObjects;

namespace RealState.Domain.Entities
{
    public class Owner : AggregateRoot
    {
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public string Photo { get; private set; }
        public DateTime Birthday { get; private set; }

        // Navigation property for the collection of properties owned by this owner
        public virtual ICollection<PropertyBuilding> Properties { get; private set; } 

        private Owner() { }
        public Owner(string name, Address address, string photo, DateTime birthday)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Photo = photo;
            Birthday = birthday;
            AddDomainEvent(new OwnerCreated(this));
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            Name = name;
        }

        public void ChangeAddress(Address address)
        {
            Address = address;
        }

        public void AddProperty(PropertyBuilding propertybuilding)
        {
            if (propertybuilding == null)
            {
                throw new ArgumentNullException(nameof(propertybuilding));
            }

            AddDomainEvent(new PropertyBuildingCreated(propertybuilding));
        }
    }
}