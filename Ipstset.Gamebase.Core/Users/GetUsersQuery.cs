using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core.Users
{
    public class GetUsersQuery : IQuery
    {
        //public string UserName { get; set; }
        //public string Email { get; set; }
        public string[] Roles { get; set; }

        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string[] Fields { get; set; }
    }
}
