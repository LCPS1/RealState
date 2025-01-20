using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using FluentAssertions;
using NSubstitute;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Application.Properties.Commands.PatchPriceProperty;
using RealState.Domain.Entities;
using RealState.Domain.ValueObjects;

namespace RealState.Application.UnitTests.Property.Commands.PatchPriceProperty
{
    public class PatchPricePropertyCommandHandlerTests
    {
        private readonly IPropertyBuildingRepository _propertyBuildingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PatchPricePropertyCommandHandler _handler;

        public PatchPricePropertyCommandHandlerTests()
        {
            _propertyBuildingRepository = Substitute.For<IPropertyBuildingRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new PatchPricePropertyCommandHandler(_unitOfWork, _propertyBuildingRepository);
        }

        [Fact]
        public async Task Handle_WhenPropertyExists_ShouldUpdatePriceAndReturnProperty()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var newPrice = new RealState.Application.Common.Models.PriceRequest(250000m, "USD");
            
            var command = new PatchPricePropertyCommand(
                Id: propertyId,
                Price: newPrice
            );

            var existingProperty = new PropertyBuilding(
                "Property Name",
                new Address("Street", "City", "12345"),
                new Price(200000m, "USD"),
                "Code123",
                2020,
                Guid.NewGuid()
            );

            _propertyBuildingRepository.GetByIdAsync(propertyId, Arg.Any<CancellationToken>())
                .Returns(existingProperty);

            _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
                .Returns(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Price.Amount.Should().Be(newPrice.Amount);
            result.Value.Price.Currency.Should().Be(newPrice.Currency);

            await _propertyBuildingRepository.Received(1).UpdateAsync(existingProperty, Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_WhenPropertyDoesNotExist_ShouldReturnNotFoundError()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var newPrice = new RealState.Application.Common.Models.PriceRequest(250000m, "USD");

            var command = new PatchPricePropertyCommand(
                Id: propertyId,
                Price: newPrice
            );

            _propertyBuildingRepository.GetByIdAsync(propertyId, Arg.Any<CancellationToken>())
                .Returns((PropertyBuilding)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be("Property.NotFound");
            result.FirstError.Description.Should().Be($"Property with ID {propertyId} was not found.");

            await _propertyBuildingRepository.DidNotReceive().UpdateAsync(Arg.Any<PropertyBuilding>(), Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }

    }
}