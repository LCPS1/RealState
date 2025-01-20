using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.PropertyImage.CreateImage
{
    public record CreateImageResponse
    (
        Guid Id,
        string Name,
        string? Description,
        string File,
        Guid PropertyId,
        bool IsEnabled,
        DateTime CreationDate
    );
}