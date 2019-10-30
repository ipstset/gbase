using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core.Platforms
{
    public class UpdatePlatformCommand : IDomainCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
