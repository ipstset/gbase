using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Api.Models;
using Ipstset.Core.Exceptions;
using Ipstset.Gamebase.Core.Games;

namespace GamebaseApi.Games
{
    public class GetGamesModel:IQueryModel
    {
        public int? PlatformId { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }

        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string[] Fields { get; set; }

        public static GetGamesQuery Map(GetGamesModel model)
        {
            try
            {
                return new GetGamesQuery
                {
                    PlatformId = model.PlatformId,
                    Publisher = model.Publisher,
                    Developer = model.Developer,
                    Limit = model.Limit,
                    Offset = model.Offset,
                    Fields = model.Fields
                };
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }
        }
    }
}
