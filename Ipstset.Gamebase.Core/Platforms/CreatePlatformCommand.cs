using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core.Platforms
{
    public class CreatePlatformCommand: IDomainCommand
    {
        public string Name { get; set; }
    }
}
