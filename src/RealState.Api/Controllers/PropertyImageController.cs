using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.PropertyImages.Commands;
using RealState.Contracts.PropertyImage.CreateImage;

namespace RealState.Api.Controllers
{
    [Route("propertyimages")]
    public class PropertyImageController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        public PropertyImageController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage([FromBody] CreateImageRequest request)
        {
            var command = request.Adapt<CreatePropertyImageCommand>();
            var result = await _mediator.Send(command);

            return result.Match(
                image => Ok(image.Adapt<CreateImageResponse>()),
                errors => Problem(errors)
            );
        }

        
    }
}