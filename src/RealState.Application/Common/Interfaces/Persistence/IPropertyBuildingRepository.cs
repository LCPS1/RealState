using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain;
using RealState.Domain.Entities;

namespace RealState.Application.Common.Interfaces.Persistence
{
    public interface IPropertyBuildingRepository
    {
        Task AddAsync(PropertyBuilding propertyBuilding,CancellationToken cancellationToken = default);
        Task UpdateAsync(PropertyBuilding propertyBuilding,CancellationToken cancellationToken = default);
        Task<PropertyBuilding?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<PropertyBuilding>> GetPropertiesWithFilters 
        (
            string? name,
            decimal? minPrice,
            decimal? maxPrice,
            string? city,
            int? year,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default
        );
    }
}