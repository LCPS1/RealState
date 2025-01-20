using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Application.PropertyImages.Commands;
using RealState.Domain.Entities;

// Aliases to resolve ambiguity
using Image = RealState.Domain.Entities;


namespace RealState.Application.PropertyImage.Commands
{
    public class CreatePropertyImageCommandHandler : IRequestHandler<CreatePropertyImageCommand, ErrorOr<Image.PropertyImage>>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;    
        private readonly IUnitOfWork _unitOfWork;
        public CreatePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository, IUnitOfWork unitOfWork)
        {
            _propertyImageRepository = propertyImageRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Image.PropertyImage>> Handle(CreatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            // Create the PropertyImage using the static factory method with validation
      // Use the static factory method correctly to create PropertyImage
            var propertyImage = Image.PropertyImage.Create(
                request.Name,
                request.Description, 
                request.File, 
                request.IsEnabled, 
                request.Id
            );
            
            // Persist the created PropertyImage entity
            await _propertyImageRepository.AddAsync(propertyImage, cancellationToken);
            
            // Save the changes to the database
            await _unitOfWork.SaveChangesAsync();

            // Return the created PropertyImage as the result
            return propertyImage;



        }
    }
}