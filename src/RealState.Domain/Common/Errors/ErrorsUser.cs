using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace RealState.Domain.Common.Errors
{
    public static class ErrorsUser
    {
        public static Error DuplicateEmail => Error.Conflict
        (
            code:"Duplicate_email", 
            description:"The email is already in use"
        );
    }
}