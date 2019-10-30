using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.Core
{
    public interface IQuery
    {
        //pagination
        int? Limit { get; set; }
        int? Offset { get; set; }

        //sorting

        //fields
        string[] Fields { get; set; }
    }
}
