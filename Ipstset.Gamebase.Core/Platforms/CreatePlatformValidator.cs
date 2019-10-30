using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ipstset.Core.Validation;

namespace Ipstset.Gamebase.Core.Platforms
{
    public class CreatePlatformValidator : IValidator<CreatePlatformCommand>
    {
        public bool IsInvalid(CreatePlatformCommand objToValidate)
        {
            if (String.IsNullOrWhiteSpace(objToValidate.Name))
                Errors.Add(ErrorCodes.PlatformErrors.NameRequired);

            return Errors.Any();
        }

        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    }
}
