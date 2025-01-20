using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Common.Models;

namespace RealState.Domain.ValueObjects
{
    public class Price : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; init; }
        private Price () {}
        public Price(decimal amount, string currency)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            Amount = amount;
            Currency = currency.ToUpper();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }
    }

}