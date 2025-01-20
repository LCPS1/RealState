using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Owners.CreateOwner;
using RealState.Contracts.Owners.CreateOwner;


namespace RealState.Api.Controllers
{
    [Route("properties/owners")]
    public class OwnerController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        public OwnerController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner(CreateOwnerRequest request)
        {
            var command = _mapper.Map<CreateOwnerCommand>(request);  
            
            var createOwnerResult = await _mediator.Send(command);
        
            return createOwnerResult.Match(
                owner => Ok(_mapper.Map<CreateOwnerResponse>(owner)),
                errors => Problem(errors)
            );
        }

    }
}