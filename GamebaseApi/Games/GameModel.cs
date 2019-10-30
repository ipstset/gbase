using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Gamebase.Core.Games;

namespace GamebaseApi.Games
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public int PlatformId { get; set; }
        public string Platform { get; set; }
        public string DateReleased { get; set; } 

        public static GameModel Map(Game game)
        {
            return new GameModel
            {
                Id = game.Id,
                Title = game.Title,
                Publisher = game.Publisher,
                Developer = game.Developer,
                PlatformId = game.PlatformId,
                Platform = game.Platform,
                DateReleased = game.DateReleased?.ToShortDateString()
            };
        }
    }
}
