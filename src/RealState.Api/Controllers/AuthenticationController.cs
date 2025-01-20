using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using RealState.Contracts.Authentication;
using ErrorOr;
using RealState.Domain.Common.Errors;
using RealState.Application.Authentication.Common;

using MediatR;
using RealState.Application.Authentication.Commands.Register;
using RealState.Application.Authentication.Queries.Login;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;


namespace RealState.Api.Controllers
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController: ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {   
            var command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);       

                return authResult.Match
                (
                    authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                    errors => Problem(errors)
                );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {    
            var query = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediator.Send(query); 

            if (authResult.IsError && authResult.FirstError == ErrorsAuthentication.InvalidCredentials)
            {
                return Problem
                (
                    statusCode: StatusCodes.Status401Unauthorized,
                    title : authResult.FirstError.Description
                );
            }

            return authResult.Match
            (
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors)
            );
        }

    }
}