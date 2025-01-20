using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using RealState.Application.PropertyImages.Commands;
using RealState.Contracts.PropertyImage.CreateImage;
using RealState.Domain.Entities;

namespace RealState.Api.Common.Mapping.PropertyImges
{
    public class PropertyImagesMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
                    // Map CreateImageRequest to CreateImageCommand
            config.NewConfig<CreateImageRequest, CreatePropertyImageCommand>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.File, src => src.File)
                .Map(dest => dest.IsEnabled, src => src.IsEnabled);

            // Map PropertyImage to CreateImageResponse
            config.NewConfig<PropertyImage, CreateImageResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.FileName)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.File, src => src.File)
                .Map(dest => dest.IsEnabled, src => src.IsEnabled);

        }
    }
}