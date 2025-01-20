using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Application.Properties.Queries.GetProperties;
using RealState.Domain.Entities;

namespace RealState.Application.UnitTests.Property.Queries.GetProperties
{
    public class GetPropertiesQueryHandlerTests
    {
        private readonly IPropertyBuildingRepository _propertyBuildingRepository;
        private readonly GetPropertiesQueryHandler _handler;

        public GetPropertiesQueryHandlerTests()
        {
            _propertyBuildingRepository = Substitute.For<IPropertyBuildingRepository>();
            _handler = new GetPropertiesQueryHandler(_propertyBuildingRepository);
        }

        [Fact]
        public async Task Handle_WhenValidCommand_ShouldReturnProperties()
        {
            // Arrange
            var command = new GetPropertiesQuery(
                name: "Property Name",         
                minPrice: 100000m,            
                maxPrice: 500000m,             
                city: "New York",             
                year: 2020,                    
                page: 1,                      
                pageSize: 10                  
            );

            var properties = new List<PropertyBuilding> { new PropertyBuilding("Property 1", "Address 1") };

            _propertyBuildingRepository.GetPropertiesWithFilters(
                Arg.Any<string>(), 
                Arg.Any<decimal?>(), 
                Arg.Any<decimal?>(), 
                Arg.Any<string>(), 
                Arg.Any<int?>(), 
                Arg.Any<int>(), 
                Arg.Any<int>()
            ).Returns(properties);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNullOrEmpty();
            result.Value.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handle_WhenNoPropertiesMatch_ShouldReturnError()
        {
            // Arrange
            var command = new GetPropertiesQuery(
                name: null,        
                minPrice: null,    
                maxPrice: null,    
                city: null,        
                year: null,        
                page: 1,          
                pageSize: 10       
            );

            _propertyBuildingRepository.GetPropertiesWithFilters(
                Arg.Any<string>(),
                Arg.Any<decimal?>(),
                Arg.Any<decimal?>(),
                Arg.Any<string>(),
                Arg.Any<int?>(),
                Arg.Any<int>(),
                Arg.Any<int>()
            ).Returns(new List<PropertyBuilding>());

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Code.Should().Be("Properties.NotFound");
        }
    }
}