using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealState.Api.Controllers
{
    [Route("properties/traces")]
    public class PropertyTraceController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        public PropertyTraceController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }



    }
}