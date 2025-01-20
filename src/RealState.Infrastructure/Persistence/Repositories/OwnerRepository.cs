using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly RealStateDbContext _dbContext;

        public OwnerRepository(RealStateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Owner owner, CancellationToken cancellationToken)
        {
            await _dbContext.PropertyOwners.AddAsync(owner, cancellationToken);
        }
        
    }
}