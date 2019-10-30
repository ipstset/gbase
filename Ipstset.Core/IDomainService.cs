using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.Core
{
    public interface IDomainService<T>
    {
        T Get(int id);
        IQueryResult<T> Get(IQuery query);
        T Create(IDomainCommand command);
        T Update(IDomainCommand command);
        void Delete(int id);
    }
}
