using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.Authentication
{
   public record AuthenticationResponse
    (
        Guid Id,
        string Email,
        string Token,
        string FirstName,
        string LastName
    );
}