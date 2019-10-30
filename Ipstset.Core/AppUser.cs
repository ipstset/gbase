using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ipstset.Core
{
    public class AppUser
    {
        public int UserId { get; set; }
        public string[] Roles { get; set; }
        public bool HasRole(string role)
        {
            return Roles.Contains(role);
        }

    }
}
