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

namespace RealState.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if ( _userRepository.GetUserByEmail(command.Email) != null)
            {
                return ErrorsUser.DuplicateEmail; 
            }

            var user = new User
            {
                Email = command.Email,
                Passsword = command.Password,
                FirstName = command.FirstName,
                LastName = command.LastName
            };

             _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}