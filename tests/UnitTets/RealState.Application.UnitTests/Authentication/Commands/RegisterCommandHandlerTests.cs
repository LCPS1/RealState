using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using FluentAssertions;
using NSubstitute;
using RealState.Application.Authentication.Commands.Register;
using RealState.Application.Common.Interfaces.Authentication;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Application.UnitTests.Authentication.Commands
{
    public class RegisterCommandHandlerTests
    {
         private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly RegisterCommandHandler _handler;

        public RegisterCommandHandlerTests()
        {
            _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new RegisterCommandHandler(_jwtTokenGenerator, _userRepository);
        }

        [Fact]
        public async Task Handle_WhenEmailAlreadyExists_ShouldReturnDuplicateEmailError()
        {
            // Arrange
            var command = new RegisterCommand("John", "Doe", "user@example.com", "password123");

            _userRepository.GetUserByEmail(Arg.Any<string>())
                .Returns(await Task.FromResult(new User())); // Simulate user already exists

            // Act
            var result = await _handler.Handle(command, default);

            // Assert 
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Type.Should().Be(ErrorType.Conflict);
        }
    }
}