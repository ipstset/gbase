using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.Gamebase.Core.Users;

namespace GamebaseApi.Users
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public string[] Roles { get; set; }


        public static UserModel Map(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                DateCreated = user.DateCreated,
                Roles = user.Roles
            };
        }
    }
}
