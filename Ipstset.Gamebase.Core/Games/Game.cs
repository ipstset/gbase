using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.Gamebase.Core.Games
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public int PlatformId { get; set; }
        public string Platform { get; set; }
        public DateTime? DateReleased { get; set; }
    }
}
