using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ipstset.Core;
using Ipstset.Gamebase.Core.Users;

namespace Ipstset.Gamebase.Data.Test.Repositories
{
    public class UserRepository : JsonRepository<User>, IUserRepository
    {
        private string _file = @"User.json";

        public User Get(int id)
        {
            var data = GetData();
            return data.FirstOrDefault(d => d.Id == id);
        }

        public List<User> GetAll()
        {
            var data = GetData();
            return data;
        }

        public List<User> Get(IQuery query)
        {
            //todo implement query
            var data = GetData();
            return data;
        }

        public void Add(User obj)
        {
            var items = GetData();
            obj.Id = 1;

            //get last id
            if (items.Any())
                obj.Id = items.OrderBy(m => m.Id).Last().Id + 1;

            items.Add(obj);
            SaveJsonToFile(items, GetFile(_file));
        }

        public void Update(User obj)
        {
            //delete, then add
            Delete(obj);
            var items = GetData();
            items.Add(obj);
            SaveJsonToFile(items.OrderBy(i => i.Id), GetFile(_file));
        }

        public void Delete(User obj)
        {
            var oldItems = GetData();
            var items = oldItems.Where(i => i.Id != obj.Id).ToList();
            SaveJsonToFile(items, GetFile(_file));
        }

        public User GetByUserName(string userName)
        {
            var data = GetData();
            return data.FirstOrDefault(d => string.Equals(d.UserName, userName, StringComparison.CurrentCultureIgnoreCase));
        }

        private List<User> GetData()
        {
            return Create(GetFile(_file));
        }
    }
}
