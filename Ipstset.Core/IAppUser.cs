using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Ipstset.Core
{
    public interface IAppUser
    {
        int UserId { get; set; }
        string UserName { get; set; }

        List<Claim> CreateClaims();
    }
}
