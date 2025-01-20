using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain;
using RealState.Domain.Entities;


using Entities = RealState.Domain.Entities;


namespace RealState.Application.Common.Interfaces.Persistence
{
    public interface IPropertyImageRepository
    {
        Task AddAsync(Entities.PropertyImage propertyImage,CancellationToken cancellationToken = default);

         
    }
}