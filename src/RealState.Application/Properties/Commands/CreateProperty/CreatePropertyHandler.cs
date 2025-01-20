using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Application.Properties.Commands.CreateProperty
{
    public class CreatePropertyHandler : IRequestHandler<CreatePropertyCommand, ErrorOr<PropertyBuilding>>
    {
        
        private readonly IPropertyBuildingRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePropertyHandler(IPropertyBuildingRepository propertyRepository, IUnitOfWork unitOfWork)
        {

            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<PropertyBuilding>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;


            // Create the Property entity
            var property = new PropertyBuilding(
                name: request.Name,
                address:new RealState.Domain.ValueObjects.Address(
                    street: request.Address.Street,
                    city: request.Address.City,
                    zipCode: request.Address.ZipCode),
                price: new RealState.Domain.ValueObjects.Price(
                    amount: request.Price.Amount,
                    currency: request.Price.Currency),
                codeInternal: request.CodeInternal,
                year: request.Year,
                ownerId: request.OwnerId
            );
        

            // Persist Changes
            await _propertyRepository.AddAsync(property, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync();
    
            // Return the created property
            return property;

        }
        
    }
}