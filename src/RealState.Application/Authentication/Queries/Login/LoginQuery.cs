using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Application.Authentication.Common;

namespace RealState.Application.Authentication.Queries.Login
{
    public record LoginQuery
    (
        string Email,
        string Password
    ): IRequest<ErrorOr<AuthenticationResult>>;
}