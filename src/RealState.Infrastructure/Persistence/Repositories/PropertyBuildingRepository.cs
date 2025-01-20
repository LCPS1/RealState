using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropertyBuildingRepository : IPropertyBuildingRepository
    {
        private readonly RealStateDbContext _dbContext;
    
        public PropertyBuildingRepository(RealStateDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(PropertyBuilding propertyBuilding, CancellationToken cancellationToken = default)
        {
            await _dbContext.Properties.AddAsync(propertyBuilding, cancellationToken);

        }

        public async Task UpdateAsync(PropertyBuilding propertyBuilding, CancellationToken cancellationToken = default)
        {
             _dbContext.Properties.Update(propertyBuilding);
        }

        public async Task<List<PropertyBuilding>> GetPropertiesWithFilters(
            string? name,
            decimal? minPrice,
            decimal? maxPrice,
            string? city,
            int? year,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Properties.AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price.Amount >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price.Amount <= maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(p => p.Address.City == city);

            if (year.HasValue)
                query = query.Where(p => p.Year == year.Value);

            // Apply pagination
            var properties = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return properties;
        }


        public async Task<PropertyBuilding?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Properties
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

    }
}