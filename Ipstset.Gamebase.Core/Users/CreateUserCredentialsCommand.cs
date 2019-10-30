using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core.Users
{
    public class CreateUserCredentialsCommand: IDomainCommand
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
