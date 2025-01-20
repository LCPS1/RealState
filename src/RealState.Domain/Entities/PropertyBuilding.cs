using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Common.Models;
using RealState.Domain.Events;
using RealState.Domain.ValueObjects;

namespace RealState.Domain.Entities
{
    public class PropertyBuilding : AggregateRoot
    {
        private string v1;
        private string v2;

        public string Name { get; private set; }
        public Address Address { get; private set; }
        public Price Price { get; private set; }
        public string CodeInternal { get; private set; }
        public int Year { get; private set; }
        public Guid OwnerId { get; private set; }

        //lazy Loading
        public virtual Owner Owner { get; private set; } 
        public virtual ICollection<PropertyTrace> Traces { get; private set; } 
        public virtual ICollection<PropertyImage> Images { get; private set; } 
        private PropertyBuilding() { }

        public PropertyBuilding(string name, Address address, Price price, string codeInternal, int year, Guid ownerId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Price = price;
            CodeInternal = codeInternal;
            Year = year;
            OwnerId = ownerId;

            AddDomainEvent(new PropertyBuildingCreated(this));
        }

        public PropertyBuilding(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public void ChangePrice(Price newPrice)
        {
            if (newPrice is null)
            {
                throw new ArgumentNullException(nameof(newPrice));
            }

            if (newPrice.Amount < 0)
            {
                throw new ArgumentException("Price amount cannot be negative", nameof(newPrice));
            }

            // Store old price for event
            var oldPrice = Price;

            // Update price
            Price = newPrice;

            // Raise domain event for price change
            AddDomainEvent(new PropertyPriceChanged(Id, oldPrice, newPrice));
        }

        public void AddPropertyTrace(PropertyTrace propertyTrace)
        {
            if (propertyTrace == null)
            {
                throw new ArgumentNullException(nameof(propertyTrace));
            }
        }

        public void Update(string name, Address address, Price price, string codeInternal, int year)
        {
            // Validate the inputs
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (price == null)
            {
                throw new ArgumentNullException(nameof(price));
            }

            if (string.IsNullOrWhiteSpace(codeInternal))
            {
                throw new ArgumentException("CodeInternal cannot be empty.");
            }

            Name = name;
            Address = address;
            Price = price;
            CodeInternal = codeInternal;
            Year = year;

            AddDomainEvent(new PropertyBuildingUpdated(this));
        }


        
    }
}