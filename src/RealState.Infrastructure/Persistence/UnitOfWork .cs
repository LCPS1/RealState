using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Infrastructure.Persistence.Repositories;

namespace RealState.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly RealStateDbContext _dbContext;

        public UnitOfWork(RealStateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           return await _dbContext.SaveChangesAsync();
        }
    }

}