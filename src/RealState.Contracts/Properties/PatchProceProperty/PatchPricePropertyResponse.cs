using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Contracts.Common.DTOs;

namespace RealState.Contracts.Properties.PatchProceProperty
{
    public class PatchPricePropertyResponse
    (
        IReadOnlyList<PropertyDTO> Properties,
        int TotalCount,
        int Page,
        int PageSize
    );
        
}