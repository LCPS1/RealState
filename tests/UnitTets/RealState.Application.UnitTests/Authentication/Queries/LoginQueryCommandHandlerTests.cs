using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using FluentAssertions;
using NSubstitute;
using RealState.Application.Authentication.Queries.Login;
using RealState.Application.Common.Interfaces.Authentication;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Application.UnitTests.Authentication.Queries
{
    public class LoginQueryCommandHandlerTests
    {
          private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly LoginQueryHandler _handler;

        public LoginQueryCommandHandlerTests()
        {
            _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new LoginQueryHandler(_jwtTokenGenerator, _userRepository);
        }

        [Fact]
        public async Task Handle_WhenUserNotFound_ShouldReturnInvalidCredentialsError()
        {
            // Arrange
            var query = new LoginQuery("user@example.com", "correctpassword");

            _userRepository.GetUserByEmail(Arg.Any<string>())
                .Returns(await Task.FromResult<User?>(null)); // Simulate user not found

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Type.Should().Be(ErrorType.Validation);
        }

        [Fact]
        public async Task Handle_WhenPasswordIncorrect_ShouldReturnInvalidCredentialsError()
        {
            // Arrange
            var query = new LoginQuery("user@example.com", "wrongpassword");
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "user@example.com",
                Passsword = "correctpassword" // Password mismatch
            };

            _userRepository.GetUserByEmail(Arg.Any<string>())
                .Returns(await Task.FromResult(user)); // Simulate user found

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Type.Should().Be(ErrorType.Validation);
        }

        [Fact]
        public async Task Handle_WhenUserFoundAndPasswordCorrect_ShouldReturnAuthenticationResult()
        {
            // Arrange
            var query = new LoginQuery("user@example.com", "correctpassword");
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "user@example.com",
                Passsword = "correctpassword" // Password matches
            };

            _userRepository.GetUserByEmail(Arg.Any<string>())
                .Returns(await Task.FromResult(user)); // Simulate user found
            _jwtTokenGenerator.GenerateToken(user).Returns("mock-token");

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Token.Should().Be("mock-token");
            result.Value.User.Should().Be(user);
        }
    }
}