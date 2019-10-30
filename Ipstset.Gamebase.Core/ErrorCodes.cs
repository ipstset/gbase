using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core.Validation;

namespace Ipstset.Gamebase.Core
{
    public class ErrorCodes
    {
        public class GameErrors
        {
            public static readonly ValidationError UniqueId = new ValidationError { Code = "2001", Message = "ID is not unique.", Name = "Id" };
            public static readonly ValidationError TitleRequired = new ValidationError { Code = "2002", Message = "Title cannot be blank.", Name = "Title" };
        }

        public class PlatformErrors
        {
            public static readonly ValidationError Id = new ValidationError { Code = "3001", Message = "ID is not unique.", Name = "Id" };
            public static readonly ValidationError NameRequired = new ValidationError { Code = "3002", Message = "Name cannot be blank.", Name = "Name" };
        }

        public class UserErrors
        {
            public static readonly ValidationError Id = new ValidationError { Code = "4001", Message = "ID is not unique.", Name = "Id" };
            public static readonly ValidationError UserNameRequired = new ValidationError { Code = "4002", Message = "Username cannot be blank.", Name = "UserName" };
            public static readonly ValidationError UserNameUnique = new ValidationError { Code = "4003", Message = "Username must be unique.", Name = "UserName" };
            public static readonly ValidationError EmailRequired = new ValidationError { Code = "4004", Message = "Email is required.", Name = "Email" };
            public static readonly ValidationError PasswordRequired = new ValidationError { Code = "4005", Message = "Password is required", Name = "Password" };
            public static readonly ValidationError PasswordNotValid = new ValidationError { Code = "4006", Message = "Password must be at least 8 characters and not be a common password.", Name = "Password" };
        }


    }
}
