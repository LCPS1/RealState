using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Application.PropertyImage.Commands;
using RealState.Application.PropertyImages.Commands;

// Aliases to resolve ambiguity
using Entities = RealState.Domain.Entities;


namespace RealState.Application.UnitTests.PropertyImage.Commands.CreatePropertyImage
{
    public class CreatePropertyImageCommandHandlerTests
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreatePropertyImageCommandHandler _handler;

        public CreatePropertyImageCommandHandlerTests()
        {
            _propertyImageRepository = Substitute.For<IPropertyImageRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new CreatePropertyImageCommandHandler(_propertyImageRepository, _unitOfWork);
        }

        [Fact]
        public async Task Handle_WhenValidCommand_ShouldCreateAndReturnPropertyImage()
        {
            // Arrange
            var command = new CreatePropertyImageCommand(
                Name: "Property Image Name",            
                Description: "Property Image Description", 
                File: "image-file-path.jpg",               
                Id: Guid.NewGuid(),                       
                IsEnabled: true                            
            );

            var propertyImage = Entities.PropertyImage.Create(
                command.Name, 
                command.Description, 
                command.File, 
                command.IsEnabled, 
                command.Id
            );

            _propertyImageRepository.AddAsync(Arg.Any<Entities.PropertyImage>(), Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);  

            _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1); 

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.FileName.Should().Be(command.Name);  
            result.Value.Description.Should().Be(command.Description);
            result.Value.File.Should().Be(command.File);  
            result.Value.IsEnabled.Should().Be(command.IsEnabled);  //
        }

        [Fact]
        public async Task Handle_WhenRepositoryThrows_ShouldReturnError()
        {
            // Arrange
            var command = new CreatePropertyImageCommand(
                Name: "Test Image",                 
                Description: "Test Description",    
                File: "testfile.jpg",            
                Id: Guid.NewGuid(),                
                IsEnabled: true                    
            );

             _propertyImageRepository
            .   AddAsync(Arg.Any<Entities.PropertyImage>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromException(new Exception("Database error")));

            // Act
            Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(action);
            exception.Message.Should().Be("Database error");

            await _propertyImageRepository.Received(1).AddAsync(Arg.Any<Entities.PropertyImage>(), Arg.Any<CancellationToken>());
            await _unitOfWork.DidNotReceive().SaveChangesAsync();
        }

    }
}