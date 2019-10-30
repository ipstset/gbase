using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ipstset.Core.Validation;

namespace Ipstset.Gamebase.Core.Users
{
    public class CreateUserValidator : IValidator<CreateUserCommand>
    {
        public bool IsInvalid(CreateUserCommand objToValidate)
        {
            if (String.IsNullOrWhiteSpace(objToValidate.UserName))
                Errors.Add(ErrorCodes.UserErrors.UserNameRequired);
            if (String.IsNullOrWhiteSpace(objToValidate.Email))
                Errors.Add(ErrorCodes.UserErrors.EmailRequired);

            return Errors.Any();
        }

        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    }
}
