using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Domain.Entities;

namespace RealState.Application.Owners.CreateOwner
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, ErrorOr<Owner>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IOwnerRepository _ownerRepository;

        public CreateOwnerCommandHandler(IUnitOfWork uniOfWork, IOwnerRepository ownerRepository)
        {
            _unitOfWork = uniOfWork;
            _ownerRepository = ownerRepository;
        }


        public async Task<ErrorOr<Owner>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var owner = new Owner(
                    name: request.Name,
                    address: new RealState.Domain.ValueObjects.Address(
                        street: request.Address.Street,
                        city: request.Address.City,
                        zipCode: request.Address.ZipCode),
                    photo: request.Photo ?? "",
                    birthday: request.Birthday
                    );

            // Use UnitOfWork to add property
            await _ownerRepository.AddAsync(owner);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Return 
            return owner;
        }

        
    }
}