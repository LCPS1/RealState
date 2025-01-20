using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using RealState.Contracts.Owners.CreateOwner;
using RealState.Application.Owners;
using RealState.Application.Owners.CreateOwner;
using RealState.Domain.Entities;
using Address = RealState.Contracts.Owners.CreateOwner.CreateOwnerAddressRequest;
using RealState.Contracts.Properties.UpdateProperty;
using RealState.Application.Properties.Commands.UpdateProperty;
using RealState.Contracts.Properties.CreateProperty;
using RealState.Application.Properties.Commands.CreateProperty;


namespace RealState.Api.Common.Mapping.Owners
{

    public class OwnerMappingConfig  : IRegister
    {
        
        public void Register(TypeAdapterConfig config)
        {
            
            // Request -> Command mapping
            config.NewConfig<CreateOwnerRequest, CreateOwnerCommand>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Birthday, src => src.Birthday)
                .Map(dest => dest.Photo, src => src.Photo)
                .Map(dest => dest.Address, src => new Address(
                    src.Address.Street,
                    src.Address.City,
                    src.Address.ZipCode));

            // Domain -> Response mapping
            config.NewConfig<Owner, CreateOwnerResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Birthday, src => src.Birthday)
                .Map(dest => dest.Address, src => new CreateOwnerAddressResponse(
                    src.Address.Street,
                    src.Address.City,
                    src.Address.ZipCode));          

        }
        
    }

    
}