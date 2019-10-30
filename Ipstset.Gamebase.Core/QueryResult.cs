using System;
using System.Collections.Generic;
using System.Text;
using Ipstset.Core;

namespace Ipstset.Gamebase.Core
{
    public class QueryResult<T>: IQueryResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
