using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ipstset.Core.Validation;

namespace Ipstset.Gamebase.Core.Games
{
    public class CreateGameValidator : IValidator<CreateGameCommand>
    {
        public bool IsInvalid(CreateGameCommand objToValidate)
        {
            if (String.IsNullOrWhiteSpace(objToValidate.Title))
                Errors.Add(ErrorCodes.GameErrors.TitleRequired);

            return Errors.Any();
        }

        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    }
}
