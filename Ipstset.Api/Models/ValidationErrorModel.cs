using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core.Validation;

namespace Ipstset.Api.Models
{
    public class ValidationErrorModel
    {
        public int Status { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }
    }
}
