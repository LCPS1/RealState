using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using RealState.Application.Authentication.Commands.Register;
using RealState.Application.Authentication.Common;
using RealState.Application.Authentication.Queries.Login;
using RealState.Contracts.Authentication;

namespace RealState.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult,AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest , src => src.User);


            config.NewConfig<RegisterRequest,RegisterCommand>();

            config.NewConfig<LoginRequest,LoginQuery>();

        }
    }
}