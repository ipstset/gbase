using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.Api.Models
{
    public interface IQueryModel
    {
        //pagination
        int? Limit { get; set; }
        int? Offset { get; set; }

        //sorting

        //fields
        string[] Fields { get; set; }
    }
}
