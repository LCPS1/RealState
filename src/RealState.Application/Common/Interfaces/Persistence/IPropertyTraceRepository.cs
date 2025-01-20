using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Entities;

namespace RealState.Application.Common.Interfaces.Persistence
{
    public interface IPropertyTraceRepository
    {
        Task AddAsync(PropertyTrace propertyTrace, CancellationToken cancellationToken = default);
    }
}