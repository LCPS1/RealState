using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Application.Common.Models
{
   public record AddressRequest
    (
        string Street,
        string City ,
        string ZipCode 
    );
}