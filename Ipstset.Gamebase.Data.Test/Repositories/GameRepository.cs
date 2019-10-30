using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ipstset.Core;
using Ipstset.Gamebase.Core.Games;

namespace Ipstset.Gamebase.Data.Test.Repositories
{
    public class GameRepository: JsonRepository<Game>,IGameRepository
    {
        private string _file = @"Game.json";

        public Game Get(int id)
        {
            var data = GetData();
            return data.FirstOrDefault(d => d.Id == id);
        }

        public List<Game> GetAll()
        {
            var data = GetData();
            return data;
        }

        public List<Game> Get(IQuery query)
        {
            //todo implement query
            var data = GetData();
            return data;
        }

        public void Add(Game obj)
        {
            var items = GetData();
            obj.Id = 1;

            //get last id
            if (items.Any())
                obj.Id = items.OrderBy(m => m.Id).Last().Id + 1;

            items.Add(obj);
            SaveJsonToFile(items, GetFile(_file));
        }

        public void Update(Game obj)
        {
            //delete, then add
            Delete(obj);
            var items = GetData();
            items.Add(obj);
            SaveJsonToFile(items.OrderBy(i => i.Id), GetFile(_file));
        }

        public void Delete(Game obj)
        {
            var oldItems = GetData();
            var items = oldItems.Where(i => i.Id != obj.Id).ToList();
            SaveJsonToFile(items, GetFile(_file));
        }

        private List<Game> GetData()
        {
            //return Create(GetFile(_file));
            var games = Create(GetFile(_file));
            var platformRepository = new PlatformRepository();
            var platforms = platformRepository.GetAll();
            foreach (var game in games)
            {
                var platform = platforms.FirstOrDefault(p => p.Id == game.PlatformId);
                if(platform!=null)
                    game.Platform = platform.Name;
            }


            return games;
        }
    }
}
