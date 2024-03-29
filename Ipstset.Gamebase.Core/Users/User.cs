﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.Gamebase.Core.Users
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public string[] Roles { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
