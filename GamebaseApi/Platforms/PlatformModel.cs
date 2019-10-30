using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Gamebase.Core.Platforms;

namespace GamebaseApi.Platforms
{
    public class PlatformModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static PlatformModel Map(Platform platform)
        {
            return new PlatformModel
            {
                Id = platform.Id,
                Name = platform.Name
            };
        }
    }
}
