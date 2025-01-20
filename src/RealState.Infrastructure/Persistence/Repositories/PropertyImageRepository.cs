using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly RealStateDbContext _dbContext;

        public PropertyImageRepository(RealStateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(PropertyImage propertyImage, CancellationToken cancellationToken = default)
        {
            await _dbContext.PropertyImages.AddAsync(propertyImage, cancellationToken);
        }
    }
}