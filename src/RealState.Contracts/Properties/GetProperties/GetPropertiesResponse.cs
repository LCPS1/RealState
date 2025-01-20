using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Contracts.Common.DTOs;

namespace RealState.Contracts.Properties.GetProperties
{
    public record GetPropertiesResponse
    (
        IReadOnlyList<PropertyDTO> Properties,
        int TotalCount,
        int Page,
        int PageSize
    );
}