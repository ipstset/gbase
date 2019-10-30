﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Core.Exceptions;
using Ipstset.Gamebase.Core.Users;

namespace GamebaseApi.Users
{
    public class CreateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public string Password { get; set; }

        public static CreateUserCommand Map(CreateUserModel model)
        {
            try
            {
                return new CreateUserCommand
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    Roles = model.Roles,
                    Password = model.Password
                };
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }

        }
    }
}
