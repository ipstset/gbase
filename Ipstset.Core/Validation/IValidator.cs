using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.Core.Validation
{
    public interface IValidator<T>
    {
        bool IsInvalid(T objToValidate);
        List<ValidationError> Errors { get; set; }
    }
}
