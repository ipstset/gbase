using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Ipstset.Core;
using Ipstset.Gamebase.Core.Games;
using Ipstset.Gamebase.Core.Platforms;

namespace Ipstset.Gamebase.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private string _connection;
        public GameRepository(string connection)
        {
            _connection = connection;
        }

        public Game Get(int id)
        {
            Game game;
            var sql = "exec game_get @id";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                game = sqlConnection.QuerySingleOrDefaultAsync<Game>(sql, new { id }).Result;
            }

            return game;
        }

        public List<Game> GetAll()
        {
            List<Game> games;
            var sql = "exec game_get @id=null";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                games = sqlConnection.QueryAsync<Game>(sql).Result.ToList();
            }
            return games;
        }

        public List<Game> Get(IQuery query)
        {
            return GetAll();
        }

        public void Add(Game obj)
        {
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();

                var p = new DynamicParameters();
                p.Add("title", obj.Title);
                p.Add("publisher", obj.Publisher);
                p.Add("developer", obj.Developer);
                p.Add("dateReleased", obj.DateReleased);
                p.Add("platformId", obj.PlatformId);
                p.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                sqlConnection.Execute("game_insert", p, commandType: CommandType.StoredProcedure);
                obj.Id = p.Get<int>("id");
            }
        }

        public void Update(Game obj)
        {
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();

                var p = new DynamicParameters();
                p.Add("id", obj.Id);
                p.Add("title", obj.Title);
                p.Add("publisher", obj.Publisher);
                p.Add("developer", obj.Developer);
                p.Add("dateReleased", obj.DateReleased);
                p.Add("platformId", obj.PlatformId);

                sqlConnection.Execute("game_update", p, commandType: CommandType.StoredProcedure);
            }
        }

        public void Delete(Game obj)
        {
            var sql = "exec game_delete @id";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                sqlConnection.Execute(sql, new { id = obj.Id });
            }
        }
    }
}
