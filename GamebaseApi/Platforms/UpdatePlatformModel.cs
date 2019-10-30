using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Core.Exceptions;
using Ipstset.Gamebase.Core.Platforms;

namespace GamebaseApi.Platforms
{
    public class UpdatePlatformModel
    {
        public string Name { get; set; }

        public static UpdatePlatformCommand Map(UpdatePlatformModel model)
        {
            try
            {
                return new UpdatePlatformCommand
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
