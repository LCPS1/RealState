using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Queries.GetProperties
{
    public class GetPropertiesQuery : IRequest<ErrorOr<List<PropertyBuilding>>>
    {
        public string? Name { get; set;}
        public decimal? MinPrice { get; set;}
        public decimal? MaxPrice { get; set;}
        public string? City { get; set;}
        public int? Year { get; set;}
        public int Page { get; set;}
        public int PageSize { get; set;}

        public GetPropertiesQuery(
            string? name, 
            decimal? minPrice, 
            decimal? maxPrice, 
            string? city, 
            int? year, 
            int page, 
            int pageSize)
        {
            Name = name;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            City = city;
            Year = year;
            Page = page;
            PageSize = pageSize;
        }
    }
}