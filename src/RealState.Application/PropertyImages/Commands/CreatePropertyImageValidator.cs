using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RealState.Domain.Entities;


// Aliases to resolve ambiguity
using Image = RealState.Domain.Entities;

namespace RealState.Application.PropertyImage.Commands
{
    public class CreatePropertyImageValidator : AbstractValidator<Image.PropertyImage>
    {
        public CreatePropertyImageValidator()
        {
            
            RuleFor(x => x.FileName).NotEmpty().WithMessage("The image name is required.")
                .MinimumLength(3).WithMessage("The image name must be at least 3 characters long.");

            RuleFor(image => image.Description)
                .MaximumLength(500).WithMessage("The description cannot exceed 500 characters.");

            RuleFor(image => image.File)
                .NotEmpty().WithMessage("The file path or reference is required.");

            RuleFor(image => image.IsEnabled)
                .NotNull().WithMessage("The IsEnabled status must be provided.");
        }
        
    }
}