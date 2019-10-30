using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Ipstset.Core;
using Ipstset.Gamebase.Core.Platforms;

namespace Ipstset.Gamebase.Data.Repositories
{
    public class PlatformRepository: IPlatformRepository
    {
        private string _connection;
        public PlatformRepository(string connection)
        {
            _connection = connection;
        }

        public Platform Get(int id)
        {
            Platform platform;
            var sql = "exec platform_get @id";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                platform = sqlConnection.QuerySingleOrDefaultAsync<Platform>(sql, new { id }).Result;
            }

            return platform;
        }

        public List<Platform> GetAll()
        {
            List<Platform> platforms;
            var sql = "exec platform_get @id=null";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                platforms = sqlConnection.QueryAsync<Platform>(sql).Result.ToList();
            }
            return platforms;
        }

        public List<Platform> Get(IQuery query)
        {
            return GetAll();
        }

        public void Add(Platform obj)
        {
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();

                var p = new DynamicParameters();
                p.Add("name", obj.Name);
                p.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                sqlConnection.Execute("platform_insert", p, commandType: CommandType.StoredProcedure);
                obj.Id = p.Get<int>("id");
            }
        }

        public void Update(Platform obj)
        {
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();

                var p = new DynamicParameters();
                p.Add("id", obj.Id);
                p.Add("name", obj.Name);

                sqlConnection.Execute("platform_update", p, commandType: CommandType.StoredProcedure);
            }
        }

        public void Delete(Platform obj)
        {
            var sql = "exec platform_delete @id";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                sqlConnection.Execute(sql, new { id = obj.Id });
            }
        }
    }
}
