using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using RealState.Application.Common.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace RealState.Api.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var ExceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, message) = ExceptionDetails switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

            return Problem(statusCode: statusCode, title: message);    
        } 

    } 
}