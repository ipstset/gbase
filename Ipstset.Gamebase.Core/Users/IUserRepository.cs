using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core.Users
{
    public interface IUserRepository: IDomainRepository<User>
    {
        User GetByUserName(string userName);
    }
}
