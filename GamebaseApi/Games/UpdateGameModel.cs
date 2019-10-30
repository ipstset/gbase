using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Core.Exceptions;
using Ipstset.Gamebase.Core.Games;

namespace GamebaseApi.Games
{
    public class UpdateGameModel
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public int PlatformId { get; set; }
        public string DateReleased { get; set; }
        public static UpdateGameCommand Map(UpdateGameModel model)
        {
            try
            {
                return new UpdateGameCommand
                {
                    Title = model.Title,
                    Publisher = model.Publisher,
                    Developer = model.Developer,
                    PlatformId = model.PlatformId,
                    DateReleased = !string.IsNullOrWhiteSpace(model.DateReleased)
                        ? DateTime.Parse(model.DateReleased)
                        : (DateTime?)null
                };
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }
        }
    }
}
