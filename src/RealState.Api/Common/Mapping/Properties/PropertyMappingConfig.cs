using System;
using System.Collections.Generic;
using Mapster;
using RealState.Application.Properties.Commands.CreateProperty;
using RealState.Application.Properties.Commands.UpdateProperty;
using RealState.Application.Properties.Commands.PatchPriceProperty; 
using RealState.Application.Properties.Queries.GetProperties;
using RealState.Contracts.Common.DTOs;
using RealState.Contracts.Properties.CreateProperty;
using RealState.Contracts.Properties.GetProperties;
using RealState.Contracts.Properties.UpdateProperty;
using RealState.Domain.Entities;
using RealState.Domain.ValueObjects;
using RealState.Application.Common.Models;

// Aliases to resolve ambiguity

using RealState.Contracts.Properties.PatchProceProperty; 

namespace RealState.Api.Common.Mapping.Properties
{
    public class PropertyMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            ConfigureValueObjects(config);
            ConfigureCommands(config);
            ConfigureQueries(config);
            ConfigureResponses(config);
        }

        private void ConfigureValueObjects(TypeAdapterConfig config)
        {
            // Address Mapping
            config.NewConfig<AddressDTO, AddressRequest>()
                .MapWith(src => new AddressRequest(src.Street, src.City, src.ZipCode));

            config.NewConfig<AddressRequest, AddressDTO>()
                .MapWith(src => new AddressDTO(src.Street, src.City, src.ZipCode));

            // Price Mapping for Create Property
            config.NewConfig<PriceDTO, PriceRequest>()
                .MapWith(src => new PriceRequest(src.Amount, src.Currency));

            config.NewConfig<PriceRequest, PriceDTO>()
                .MapWith(src => new PriceDTO(src.Amount, src.Currency));

            // Price Mapping for Patch Price Property (New Mapping)
            config.NewConfig<PriceRequest, RealState.Domain.ValueObjects.Price>()
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.Currency, src => src.Currency);
        }

        private void ConfigureCommands(TypeAdapterConfig config)
        {
            // Create Property Command Mapping
            config.NewConfig<CreatePropertyRequest, CreatePropertyCommand>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address.Adapt<AddressRequest>())
                .Map(dest => dest.Price, src => src.Price.Adapt<PriceRequest>())
                .Map(dest => dest.CodeInternal, src => src.CodeInternal)
                .Map(dest => dest.Year, src => src.Year)
                .Map(dest => dest.OwnerId, src => src.OwnerId);

            // Update Property Command Mapping
            config.NewConfig<(Guid Id, UpdatePropertyRequest Request), UpdatePropertyCommand>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Request.Name)
                .Map(dest => dest.Address, src => src.Request.Address.Adapt<AddressRequest>())
                .Map(dest => dest.Price, src => src.Request.Price.Adapt<PriceRequest>())
                .Map(dest => dest.CodeInternal, src => src.Request.CodeInternal)
                .Map(dest => dest.Year, src => src.Request.Year)
                .Map(dest => dest.OwnerId, src => src.Request.OwnerId);

            // Patch Price Property Command Mapping (New Mapping)
            config.NewConfig<PatchPricePropertyRequest, PatchPricePropertyCommand>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Price, src => src.Price.Adapt<PriceRequest>());
        }

        private void ConfigureQueries(TypeAdapterConfig config)
        {
            // Map filters and pagination params for GetPropertiesQuery
            config.NewConfig<GetPropertiesRequest, GetPropertiesQuery>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.MinPrice, src => src.MinPrice)
                .Map(dest => dest.MaxPrice, src => src.MaxPrice)
                .Map(dest => dest.City, src => src.City)
                .Map(dest => dest.Year, src => src.Year)
                .Map(dest => dest.Page, src => src.Page)
                .Map(dest => dest.PageSize, src => src.PageSize);
        }

        private void ConfigureResponses(TypeAdapterConfig config)
        {
            // Map PropertyBuilding entity to PropertyDTO
            config.NewConfig<PropertyBuilding, PropertyDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address.Adapt<AddressDTO>())
                .Map(dest => dest.Price, src => src.Price.Adapt<PriceDTO>())
                .Map(dest => dest.CodeInternal, src => src.CodeInternal)
                .Map(dest => dest.Year, src => src.Year)
                .Map(dest => dest.OwnerId, src => src.OwnerId);

            // Map result of query with pagination to the response format
            config.NewConfig<(List<PropertyBuilding> Properties, int TotalCount), GetPropertiesResponse>()
                .Map(dest => dest.Properties, src => src.Properties.Adapt<List<PropertyDTO>>())
                .Map(dest => dest.TotalCount, src => src.TotalCount);


               // Map PropertyBuilding entity to PropertyDTO
            config.NewConfig<PropertyBuilding, PropertyDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address.Adapt<AddressDTO>())
                .Map(dest => dest.Price, src => src.Price.Adapt<PriceDTO>())
                .Map(dest => dest.CodeInternal, src => src.CodeInternal)
                .Map(dest => dest.Year, src => src.Year)
                .Map(dest => dest.OwnerId, src => src.OwnerId);

            // Map List<PropertyBuilding> to GetPropertiesResponse
            config.NewConfig<List<PropertyBuilding>, GetPropertiesResponse>()
                .Map(dest => dest.Properties, src => src.Adapt<List<PropertyDTO>>());
        }
    }
}