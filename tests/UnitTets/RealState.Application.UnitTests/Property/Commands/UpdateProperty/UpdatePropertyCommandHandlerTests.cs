using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Application.Properties.Commands.UpdateProperty;
using RealState.Domain.Entities;
using RealState.Domain.ValueObjects;

namespace RealState.Application.UnitTests.Property.Commands.UpdateProperty
{
    public class UpdatePropertyCommandHandlerTests
    {
       private readonly IPropertyBuildingRepository _propertyBuildingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UpdatePropertyCommandHandler _handler;

        public UpdatePropertyCommandHandlerTests()
        {
            _propertyBuildingRepository = Substitute.For<IPropertyBuildingRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new UpdatePropertyCommandHandler(_unitOfWork, _propertyBuildingRepository);
        }

        [Fact]
        public async Task Handle_WhenPropertyExists_ShouldUpdateAndReturnProperty()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();

            var command = new UpdatePropertyCommand(
                Id: propertyId,
                Name: "Updated Property",
                Address: new RealState.Application.Common.Models.AddressRequest(
                    "Updated Street", "Updated City", "54321"
                ),
                Price: new RealState.Application.Common.Models.PriceRequest(
                    200000m, "EUR"
                ),
                CodeInternal: "NEW123",
                Year: 2023,
                OwnerId: ownerId
            );

            var existingProperty = new PropertyBuilding(
                "Original Property",
                new Address("Original Street", "Original City", "12345"),
                new Price(100000m, "USD"),
                "OLD123",
                2020,
                ownerId
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
            result.Value.Name.Should().Be(command.Name);
            result.Value.Address.Street.Should().Be(command.Address.Street);
            result.Value.Address.City.Should().Be(command.Address.City);
            result.Value.Address.ZipCode.Should().Be(command.Address.ZipCode);
            result.Value.Price.Amount.Should().Be(command.Price.Amount);
            result.Value.Price.Currency.Should().Be(command.Price.Currency);
            result.Value.CodeInternal.Should().Be(command.CodeInternal);
            result.Value.Year.Should().Be(command.Year);

            await _propertyBuildingRepository.Received(1).UpdateAsync(existingProperty, Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_WhenPropertyDoesNotExist_ShouldReturnNotFoundError()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();

            var command = new UpdatePropertyCommand(
                Id: propertyId,
                Name: "Updated Property",
                Address: new RealState.Application.Common.Models.AddressRequest(
                    "Updated Street", "Updated City", "54321"
                ),
                Price: new RealState.Application.Common.Models.PriceRequest(
                    200000m, "EUR"
                ),
                CodeInternal: "NEW123",
                Year: 2023,
                OwnerId: ownerId
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