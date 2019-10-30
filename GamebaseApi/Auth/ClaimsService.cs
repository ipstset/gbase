using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using Ipstset.Core;
using Ipstset.Gamebase.Core.Users;

namespace GamebaseApi.Auth
{
    public class ClaimsService
    {
        public static IEnumerable<Claim> Create(User user)
        {
            //map claims here
            return new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim(JwtClaimTypes.GivenName, user.FirstName ?? ""),
                new Claim(JwtClaimTypes.FamilyName, user.LastName ?? ""),
                new Claim(JwtClaimTypes.Email, user.Email ?? ""),
                new Claim("roles",string.Join(",", user.Roles))
            };
        }

        public static AppUser CreateAppUser(IEnumerable<Claim> claims)
        {
            var claimList = claims.ToList();
            return new AppUser
            {
                UserId = Convert.ToInt32(claimList.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject)?.Value),
                Roles = claimList.FirstOrDefault(c => c.Type == "roles")?.Value.Split(",")
            };
        }
    }
}
