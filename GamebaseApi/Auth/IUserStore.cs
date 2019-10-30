using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ipstset.Gamebase.Core.Users;

namespace GamebaseApi.Auth
{
    public interface IUserStore
    {
        bool ValidateCredentials(string username, string password);
        Task<User> FindBySubjectId(string subjectId);
        User FindByUsername(string username);
        User FindByExternalProvider(string provider, string subjectId);
        User AutoProvisionUser(string provider, string subjectId, List<Claim> claims);
    }
}
