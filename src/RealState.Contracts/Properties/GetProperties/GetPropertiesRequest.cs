using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Contracts.Properties.GetProperties
{
   public record GetPropertiesRequest
   (
      string? Name = null,
      decimal? MinPrice = null,
      decimal? MaxPrice = null,
      string? City = null,
      int? Year = null,
      int Page = 1,
      int PageSize = 10
   );
}