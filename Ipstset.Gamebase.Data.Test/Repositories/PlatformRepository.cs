using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ipstset.Core;
using Ipstset.Gamebase.Core.Platforms;

namespace Ipstset.Gamebase.Data.Test.Repositories
{
    public class PlatformRepository: JsonRepository<Platform>,IPlatformRepository
    {
        private string _file = @"Platform.json";

        public Platform Get(int id)
        {
            var data = GetData();
            return data.FirstOrDefault(d => d.Id == id);
        }

        public List<Platform> GetAll()
        {
            var data = GetData();
            return data;
        }

        public List<Platform> Get(IQuery query)
        {
            //todo implement query
            var data = GetData();
            return data;
        }

        public void Add(Platform obj)
        {
            var items = GetData();
            obj.Id = 1;

            //get last id
            if (items.Any())
                obj.Id = items.OrderBy(m => m.Id).Last().Id + 1;

            items.Add(obj);
            SaveJsonToFile(items, GetFile(_file));
        }

        public void Update(Platform obj)
        {
            //delete, then add
            Delete(obj);
            var items = GetData();
            items.Add(obj);
            SaveJsonToFile(items.OrderBy(i => i.Id), GetFile(_file));
        }

        public void Delete(Platform obj)
        {
            var oldItems = GetData();
            var items = oldItems.Where(i => i.Id != obj.Id).ToList();
            SaveJsonToFile(items, GetFile(_file));
        }

        private List<Platform> GetData()
        {
            return Create(GetFile(_file));
        }
    }
}
