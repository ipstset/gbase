using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.Core
{
    public interface IDomainRepository<T>
    {
        T Get(int id);
        List<T> GetAll();
        List<T> Get(IQuery query);
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);

    }
}
