using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Application.Common.Interfaces.Authentication;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Application.Authentication.Common;
using RealState.Domain.Common.Errors;
using RealState.Domain.Entities;

namespace RealState.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        private readonly IUserRepository _userRepository;
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            try
            {
                // 1. Check if user exists
                var user =  _userRepository.GetUserByEmail(query.Email);
                if (user is null)
                {
                    return ErrorsAuthentication.InvalidCredentials;
                }
                // 2. Validate password
                if (user.Passsword != query.Password)
                {
                    return ErrorsAuthentication.InvalidCredentials;
                }

                // 3. Generate and return token
                var token = _jwtTokenGenerator.GenerateToken(user);
                return new AuthenticationResult(user, token);
            }
            catch (Exception ex)
            {
                return ErrorsAuthentication.UnexpectedError(ex.Message);
            }
        }
    }
}