using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Common.Models;

namespace RealState.Domain.ValueObjects
{
    public class Tax : ValueObject
    {
        public decimal Percentage { get; init; }
        public string Type { get; init; }
        private Tax () {}
       public Tax(decimal percentage, string type)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Tax percentage must be between 0 and 100");
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Tax type is required");

            Percentage = percentage;
            Type = type;
        }   

        public decimal CalculateTax(Price value)
        {
            return (value.Amount * Percentage) / 100;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Percentage;
        }
    }
}