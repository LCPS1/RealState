using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly RealStateDbContext _dbContext;

        public PropertyTraceRepository(RealStateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(PropertyTrace propertyTrace, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}