using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.Authentication
{
    public record LoginRequest
    (
        string Email,
        string Password
    );
}