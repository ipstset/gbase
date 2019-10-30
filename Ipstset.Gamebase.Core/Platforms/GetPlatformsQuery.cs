using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core.Platforms
{
    public class GetPlatformsQuery:IQuery
    {
        public string Name { get; set; }

        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string[] Fields { get; set; }
    }
}
