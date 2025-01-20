using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.PropertyImage.CreateImage
{
    public record CreateImageRequest
    (
        string Name,
        string? Description,
        string File,
        Guid Id,
        bool IsEnabled  
    );
}