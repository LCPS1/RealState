using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Entities;

namespace RealState.Application.Authentication.Common
{
    public record AuthenticationResult
    (
        User User,
        string Token
    );
}