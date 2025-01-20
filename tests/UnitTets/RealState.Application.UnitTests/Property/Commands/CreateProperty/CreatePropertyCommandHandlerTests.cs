using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Application.Properties.Commands.CreateProperty;
using RealState.Application.Properties.Commands.UpdateProperty;
using RealState.Domain.Entities;
using RealState.Application.Common.Models;



namespace RealState.Application.UnitTests.Property.Commands.CreateProperty
{
    public class CreatePropertyCommandHandlerTests
    {
        private readonly IPropertyBuildingRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreatePropertyHandler _handler;

        public CreatePropertyCommandHandlerTests()
        {
            _propertyRepository = Substitute.For<IPropertyBuildingRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new CreatePropertyHandler(_propertyRepository, _unitOfWork);
        }

        [Fact]
        public async Task Handle_WhenValidCommand_ShouldCreateAndReturnProperty()
        {
            // Arrange
            var command = new CreatePropertyCommand(
                Name: "Test Property",
                Address: new AddressRequest("123 Main St", "Test City", "12345"),
                Price: new PriceRequest(100000m, "USD"),
                CodeInternal: "INT123",
                Year: 2022,
                OwnerId: Guid.NewGuid()
            );

            var property = new PropertyBuilding(
                name: command.Name,
                address: new Domain.ValueObjects.Address(command.Address.Street, command.Address.City, command.Address.ZipCode),
                price: new Domain.ValueObjects.Price(command.Price.Amount, command.Price.Currency),
                codeInternal: command.CodeInternal,
                year: command.Year,
                ownerId: command.OwnerId
            );

            _propertyRepository.AddAsync(Arg.Any<PropertyBuilding>(), Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);

            _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Name.Should().Be(command.Name);
            result.Value.Address.Street.Should().Be(command.Address.Street);
            result.Value.Address.City.Should().Be(command.Address.City);
            result.Value.Address.ZipCode.Should().Be(command.Address.ZipCode);
            result.Value.Price.Amount.Should().Be(command.Price.Amount);
            result.Value.Price.Currency.Should().Be(command.Price.Currency);
            result.Value.CodeInternal.Should().Be(command.CodeInternal);
            result.Value.Year.Should().Be(command.Year);
            result.Value.OwnerId.Should().Be(command.OwnerId);
        }

        [Fact]
        public async Task Handle_WhenRepositoryThrows_ShouldReturnError()
        {
            // Arrange
            var command = new CreatePropertyCommand(
                Name: "Test Property",
                Address: new AddressRequest("123 Main St", "Test City", "12345"),
                Price: new PriceRequest(100000m, "USD"),
                CodeInternal: "INT123",
                Year: 2022,
                OwnerId: Guid.NewGuid()
            );

            _propertyRepository.AddAsync(Arg.Any<PropertyBuilding>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromException(new Exception("Database error")));

            // Act
            Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(action);
            exception.Message.Should().Be("Database error");

            await _propertyRepository.Received(1).AddAsync(Arg.Any<PropertyBuilding>(), Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().SaveChangesAsync();
        }
    }
}