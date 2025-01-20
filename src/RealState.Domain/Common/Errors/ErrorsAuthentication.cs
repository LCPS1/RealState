using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace RealState.Domain.Common.Errors
{
    public static class ErrorsAuthentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCredentials",
            description: "Invalid email or password."
        );


        public static Error UnexpectedError(string errorMessage) => Error.Failure(
            code: "Auth.UnexpectedError",
            description: errorMessage
        );
    }
}