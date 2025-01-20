using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, ErrorOr<PropertyBuilding>>
    {
        private readonly IPropertyBuildingRepository _propertyBuildingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyCommandHandler(IUnitOfWork unitOfWork, IPropertyBuildingRepository propertyBuildingRepository)
        {
            _unitOfWork = unitOfWork;
            _propertyBuildingRepository = propertyBuildingRepository;
        }

        public async Task<ErrorOr<PropertyBuilding>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
               // Retrieve the property by Id
            var property = await _propertyBuildingRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (property is null)
            {
                return Error.NotFound(
                    code: "Property.NotFound",
                    description: $"Property with ID {request.Id} was not found.");
            }


             // Map the Address object from the command to the domain Address object
            var newAddress = new RealState.Domain.ValueObjects.Address(
                request.Address.Street,
                request.Address.City,
                request.Address.ZipCode
            );

            // Map the Price object from the command to the domain Price object
            var newPrice = new RealState.Domain.ValueObjects.Price(
                request.Price.Amount,
                request.Price.Currency
            );


           // Update the property using the domain methods
            try
            {
                property.Update(
                    request.Name,
                    newAddress,
                    newPrice,
                    request.CodeInternal,
                    request.Year
                );

            // Save the updated property
            await _propertyBuildingRepository.UpdateAsync(property, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

                return property;  // Return the updated property
            }
            catch (ArgumentException ex)
            {
                return Error.Validation(ex.Message);  // Return validation error if any
            }

        }
    }
}