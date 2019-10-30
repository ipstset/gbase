using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core.Games
{
    public class GetGamesQuery: IQuery
    {
        public int? PlatformId { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }

        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string[] Fields { get; set; }
    }
}
