using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ErrorOr;
using RealState.Domain.Entities;
using RealState.Application.Common.Interfaces.Persistence;

namespace RealState.Application.Properties.Commands.PatchPriceProperty
{
    public class PatchPricePropertyCommandHandler : IRequestHandler<PatchPricePropertyCommand, ErrorOr<PropertyBuilding>>
    {
        private readonly IPropertyBuildingRepository _propertyBuildingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatchPricePropertyCommandHandler(IUnitOfWork unitOfWork, IPropertyBuildingRepository propertyBuildingRepository)
        {
            _unitOfWork = unitOfWork;
            _propertyBuildingRepository = propertyBuildingRepository;
        }

        public async Task<ErrorOr<PropertyBuilding>> Handle(PatchPricePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyBuildingRepository.GetByIdAsync(request.Id, cancellationToken);

            if (property == null)
            {
                return Error.NotFound(
                    code: "Property.NotFound",
                    description: $"Property with ID {request.Id} was not found."
                );
            }

            // Map the Price object from the command to the domain Price object
            var newPrice = new RealState.Domain.ValueObjects.Price(request.Price.Amount, request.Price.Currency);

            // Update the price in the domain entity
            property.ChangePrice(newPrice);

            // Save the changes
            await _propertyBuildingRepository.UpdateAsync(property, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return the updated property wrapped in ErrorOr
            return property; 
        }
    }
}