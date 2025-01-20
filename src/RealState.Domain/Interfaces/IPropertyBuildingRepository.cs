using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Entities;

namespace RealState.Domain.Interfaces
{
    public interface IPropertyBuildingRepository
    {
        Task AddAsync(PropertyBuilding property);
        Task<PropertyBuilding> GetByIdAsync(Guid id);
        Task<IEnumerable<PropertyBuilding>> GetAllAsync();
    }
}