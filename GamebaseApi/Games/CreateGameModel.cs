using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Api.Models;
using Ipstset.Core.Exceptions;
using Ipstset.Gamebase.Core.Games;

namespace GamebaseApi.Games
{
    public class CreateGameModel
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public int PlatformId { get; set; }
        public string DateReleased { get; set; }

        public static CreateGameCommand Map(CreateGameModel model)
        {
            try
            {
                return new CreateGameCommand
                {
                    Title = model.Title,
                    Publisher = model.Publisher,
                    Developer = model.Developer,
                    PlatformId = model.PlatformId,
                    DateReleased = !string.IsNullOrWhiteSpace(model.DateReleased)
                        ? DateTime.Parse(model.DateReleased)
                        : (DateTime?) null
                };
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }

        }
    }
}
