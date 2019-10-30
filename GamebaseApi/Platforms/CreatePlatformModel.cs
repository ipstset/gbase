using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Core.Exceptions;
using Ipstset.Gamebase.Core.Platforms;

namespace GamebaseApi.Platforms
{
    public class CreatePlatformModel
    {
        public string Name { get; set; }

        public static CreatePlatformCommand Map(CreatePlatformModel model)
        {
            try
            {
                return new CreatePlatformCommand
                {
                    Name = model.Name
                };
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }

        }
    }
}
