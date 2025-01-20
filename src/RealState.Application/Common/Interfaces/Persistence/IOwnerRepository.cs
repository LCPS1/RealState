using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Entities;

namespace RealState.Application.Common.Interfaces.Persistence
{
    public interface IOwnerRepository
    {
        Task AddAsync(Owner owner, CancellationToken cancellationToken = default);
    }
}