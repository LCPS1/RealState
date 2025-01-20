using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Entities;

namespace RealState.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        Task AddAsync(Owner owner);
        Task<Owner> GetByIdAsync(Guid id);
        Task<IEnumerable<Owner>> GetAllAsync();
    }
}