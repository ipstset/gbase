using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ipstset.Core.Validation;
using Ipstset.Gamebase.Core.Platforms;

namespace Ipstset.Gamebase.Core.Users
{
    public class UpdateUserValidator : IValidator<UpdateUserCommand>
    {
        public bool IsInvalid(UpdateUserCommand objToValidate)
        {
            if (String.IsNullOrWhiteSpace(objToValidate.Email))
                Errors.Add(ErrorCodes.UserErrors.EmailRequired);

            return Errors.Any();
        }

        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    }
}
