using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Queries.GetProperties
{
   public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, ErrorOr<List<PropertyBuilding>>>
    {
         private readonly IPropertyBuildingRepository _repository;

        public GetPropertiesQueryHandler(IPropertyBuildingRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<List<PropertyBuilding>>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _repository.GetPropertiesWithFilters(
                request.Name,
                request.MinPrice,
                request.MaxPrice,
                request.City,
                request.Year,
                request.Page,
                request.PageSize
            );

            if (properties == null || !properties.Any())
            {
                return Error.NotFound(
                    code: "Properties.NotFound",
                    description: "No properties match the given filters.");
            }

            return properties;
        }
    }
}