using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;

// Aliases to resolve ambiguity
using Entities = RealState.Domain.Entities;

namespace RealState.Application.PropertyImages.Commands
{
    public record CreatePropertyImageCommand
    (
        string Name,
        string? Description,
        string File,
        Guid Id,
        bool IsEnabled  
    ) : IRequest<ErrorOr<Entities.PropertyImage>>;
}