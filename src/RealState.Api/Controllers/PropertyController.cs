using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Properties.Commands.CreateProperty;
using RealState.Application.Properties.Commands.PatchPriceProperty;
using RealState.Application.Properties.Commands.UpdateProperty;
using RealState.Application.Properties.Queries.GetProperties;
using RealState.Contracts.Owners.CreateOwner;
using RealState.Contracts.Properties.CreateProperty;
using RealState.Contracts.Properties.GetProperties;
using RealState.Contracts.Properties.PatchProceProperty;
using RealState.Contracts.Properties.UpdateProperty;

namespace RealState.Api.Controllers
{
    [Route("properties")]
    public class PropertyController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public PropertyController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePropertyRequest request)
        {
            var command = request.Adapt<CreatePropertyCommand>();
            var result = await _mediator.Send(command);

            return result.Match(
                property => Ok(property.Adapt<CreatePropertyResponse>()),
                errors => Problem(errors));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePropertyRequest request)
        {
            var command = (id, request).Adapt<UpdatePropertyCommand>();
            var result = await _mediator.Send(command);

            return result.Match(
                property => Ok(property.Adapt<UpdatePropertyResponse>()),
                errors => Problem(errors));
        }



        [HttpPatch("{id:guid}/price")]
        public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] PatchPricePropertyRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("Id mismatch");
            }

            var command = _mapper.Map<PatchPricePropertyCommand>(request);

            var result = await _mediator.Send(command);

            return result.Match(
                property => Ok(_mapper.Map<Contracts.Common.DTOs.PropertyDTO>(property)), 
                errors => Problem(errors) 
            );
        }

        
        [HttpPost("filter")]
        public async Task<IActionResult> GetAllFiltered([FromBody] GetPropertiesRequest request)
        {
            var query = request.Adapt<GetPropertiesQuery>();
            Console.WriteLine($"Mapped Query: {query}");
            var result = await _mediator.Send(query);

            return result.Match(
                properties => Ok(properties.Adapt<GetPropertiesResponse>()),
                errors => Problem(errors));
        }
    
    }
}