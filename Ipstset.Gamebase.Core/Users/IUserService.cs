using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core.Users
{
    public interface IUserService: IDomainService<User>
    {
        User GetByUserName(string userName);
    }
}
