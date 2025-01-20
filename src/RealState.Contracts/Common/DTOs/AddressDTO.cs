using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.Common.DTOs
{
    public record AddressDTO
    (
        string Street,
        string City,
        string ZipCode
    );

}