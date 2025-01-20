using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}