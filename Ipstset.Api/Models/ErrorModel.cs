using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.Api.Models
{
    public class ErrorModel
    {
        public int Status { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }
}
