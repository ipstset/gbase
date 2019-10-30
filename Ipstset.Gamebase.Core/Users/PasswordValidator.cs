using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ipstset.Core.Validation;

namespace Ipstset.Gamebase.Core.Users
{
    public class PasswordValidator : IValidator<string>
    {
        public bool IsInvalid(string objToValidate)
        {
            if (String.IsNullOrWhiteSpace(objToValidate))
                Errors.Add(ErrorCodes.UserErrors.PasswordRequired);
            if (!String.IsNullOrWhiteSpace(objToValidate) && objToValidate.Length <= 7)
                Errors.Add(ErrorCodes.UserErrors.PasswordNotValid);

            return Errors.Any();
        }

        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    }
}
