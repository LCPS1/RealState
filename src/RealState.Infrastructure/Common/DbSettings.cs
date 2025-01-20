using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.Infrastructure.Common
{
    public class DbSettings
    {
        public const string SectionName = "DbSettings";
        public string DefaultConnectionString { get; set; } = null!;
    }
}