using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core.Games
{
    public class CreateGameCommand: IDomainCommand
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public int PlatformId { get; set; }
        public DateTime? DateReleased { get; set; }
    }
}
