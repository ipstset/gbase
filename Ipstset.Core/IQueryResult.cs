using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.Core
{
    public interface IQueryResult<T>
    {
        IEnumerable<T> Items { get; set; }
        int TotalCount { get; set; }
    }
}
