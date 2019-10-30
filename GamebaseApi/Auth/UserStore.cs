using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ipstset.Core;
using Ipstset.Core.Security;
using Ipstset.Gamebase.Core;
using Ipstset.Gamebase.Core.Users;

namespace GamebaseApi.Auth
{
    public class UserStore : IUserStore
    {
        private IUserService _userService;
        public UserStore(IUserService userService)
        {
            _userService = userService;
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = _userService.GetByUserName(username.ToLower());
            if (user == null) return false;

            var pwdHash = SaltedHash.Create(password, user.Salt, Constants.HashKey).Hash;
            return pwdHash == user.Password;
        }

        public async Task<User> FindBySubjectId(string subjectId)
        {
            int id = int.TryParse(subjectId, out id) ? id : 0;
            var user = _userService.Get(id);
            return user;
        }

        public User FindByUsername(string username)
        {
            var user = _userService.GetByUserName(username.ToLower());
            return user;
        }

        public User FindByExternalProvider(string provider, string subjectId)
        {
            throw new NotImplementedException();
        }

        public User AutoProvisionUser(string provider, string subjectId, List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        
    }
}
