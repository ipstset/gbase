using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Api.Models;
using Ipstset.Core.Exceptions;
using Ipstset.Gamebase.Core.Platforms;

namespace GamebaseApi.Platforms
{
    public class GetPlatformsModel : IQueryModel
    {
        public string Name { get; set; }

        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string[] Fields { get; set; }

        public static GetPlatformsQuery Map(GetPlatformsModel model)
        {
            try
            {
                return new GetPlatformsQuery
                {
                    Name = model.Name,
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
