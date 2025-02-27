using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using FluentValidation;

namespace RealState.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
    private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator=null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next,
             CancellationToken cancellationToken)
        {
           if(_validator is null)
           {
                return await next();
           }
            //Before Handler
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(validationResult.IsValid)
            {
                return await next();
            }
            else
            {
                //Handle Validation Errors
                var errors = validationResult.Errors
                    .ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName,validationFailure.ErrorMessage));

                return (dynamic)errors;
            }

        }
    }
}