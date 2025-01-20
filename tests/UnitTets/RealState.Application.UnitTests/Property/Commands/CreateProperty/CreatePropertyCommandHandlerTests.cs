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
using Address = RealState.Application.Properties.Commands.CreateProperty.Address;




// Aliases to resolve ambiguity
using Entities = RealState.Domain.Entities;
using Price = RealState.Application.Properties.Commands.CreateProperty.Price;


namespace RealState.Application.UnitTests.Property.Commands.CreateProperty
{
    public class CreatePropertyCommandHandlerTests
    {
        private readonly CreatePropertyHandler _handler;
        private readonly IPropertyBuildingRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        // Test data
        private static readonly Address ValidAddress = new(
            Street: "123 Main St",
            City: "Test City",
            ZipCode: "12345");

        private static readonly Price ValidPrice = new(
            Amount: 100000,
            Currency: "USD");

        private static readonly CreatePropertyCommand ValidCommand = new(
            Name: "Test Property",
            Address: new Application.Common.Models.AddressRequest(
                Street: "123 Main St",
                City: "Test City",
                ZipCode: "12345"),
            Price: new Application.Common.Models.PriceRequest(
                Amount: 100000m,
                Currency: "USD"),
            CodeInternal: "TEST123",
            Year: 2023,
            OwnerId: Guid.NewGuid());

        public CreatePropertyCommandHandlerTests()
        {
            _propertyRepository = Substitute.For<IPropertyBuildingRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new CreatePropertyHandler(_propertyRepository, _unitOfWork);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldCreateProperty()
        {
            // Arrange
            _propertyRepository
                .AddAsync(Arg.Any<PropertyBuilding>(), Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);

            _unitOfWork
                .SaveChangesAsync()
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            
            var property = result.Value;
            property.Should().NotBeNull();
            property.Name.Should().Be(ValidCommand.Name);
            property.CodeInternal.Should().Be(ValidCommand.CodeInternal);
            property.Year.Should().Be(ValidCommand.Year);
            property.OwnerId.Should().Be(ValidCommand.OwnerId);
            
            // Address assertions
            property.Address.Street.Should().Be(ValidCommand.Address.Street);
            property.Address.City.Should().Be(ValidCommand.Address.City);
            property.Address.ZipCode.Should().Be(ValidCommand.Address.ZipCode);
            
            // Price assertions
            property.Price.Amount.Should().Be(ValidCommand.Price.Amount);
            property.Price.Currency.Should().Be(ValidCommand.Price.Currency);

            // Verify repository calls
            await _propertyRepository
                .Received(1)
                .AddAsync(Arg.Is<PropertyBuilding>(p => 
                    p.Name == ValidCommand.Name &&
                    p.CodeInternal == ValidCommand.CodeInternal
                ), Arg.Any<CancellationToken>());

            await _unitOfWork
                .Received(1)
                .SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_WhenRepositoryThrows_ShouldReturnError()
        {
            // Arrange
            _propertyRepository
                .AddAsync(Arg.Any<PropertyBuilding>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromException(new Exception("Database error")));

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            
            // Verify that SaveChanges was not called
            await _unitOfWork
                .DidNotReceive()
                .SaveChangesAsync();
        }

        [Theory]
        [InlineData("", "Invalid name")]
        [InlineData(" ", "Invalid name")]
        [InlineData(null, "Invalid name")]
        public async Task Handle_WithInvalidName_ShouldReturnError(string invalidName, string expectedError)
        {
            // Arrange
            var command = ValidCommand with { Name = invalidName };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            
            // Verify no repository calls were made
            await _propertyRepository
                .DidNotReceive()
                .AddAsync(Arg.Any<PropertyBuilding>(), Arg.Any<CancellationToken>());

            await _unitOfWork
                .DidNotReceive()
                .SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_WithInvalidPrice_ShouldReturnError()
        {
            // Arrange
            var command = ValidCommand with { 
                Price = new Application.Common.Models.PriceRequest(-100m, "USD") 
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            
            await _propertyRepository
                .DidNotReceive()
                .AddAsync(Arg.Any<PropertyBuilding>(), Arg.Any<CancellationToken>());

            await _unitOfWork
                .DidNotReceive()
                .SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_WithInvalidYear_ShouldReturnError()
        {
            // Arrange
            var command = ValidCommand with { Year = DateTime.Now.Year + 1 };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            
            await _propertyRepository
                .DidNotReceive()
                .AddAsync(Arg.Any<PropertyBuilding>(), Arg.Any<CancellationToken>());

            await _unitOfWork
                .DidNotReceive()
                .SaveChangesAsync();
        }
    }
}