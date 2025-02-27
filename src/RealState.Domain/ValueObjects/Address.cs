using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Common.Models;

namespace RealState.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string ZipCode { get; }

        private Address () {}
        public Address(string street, string city, string zipCode)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return ZipCode;
        }
    }
}