using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Ipstset.Core;
using Ipstset.Gamebase.Core.Users;

namespace Ipstset.Gamebase.Data.Repositories
{
    public class UserRepository:IUserRepository
    {
        private string _connection;
        public UserRepository(string connection)
        {
            _connection = connection;
        }

        public User Get(int id)
        {
            User user;
            var sql = "exec user_get @id";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                user = sqlConnection.QuerySingleOrDefaultAsync<User>(sql, new { id }).Result;
            }

            if (user != null)
                user.Roles = GetUserRoles(user.Id);

            return user;
        }

        public List<User> GetAll()
        {
            List<User> users;
            var sql = "exec user_get @id=null";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                users = sqlConnection.QueryAsync<User>(sql).Result.ToList();
            }

            foreach (var user in users)
                user.Roles = GetUserRoles(user.Id);

            return users;
        }

        public List<User> Get(IQuery query)
        {
            return GetAll();
        }

        public void Add(User obj)
        {
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();

                var p = new DynamicParameters();
                p.Add("userName", obj.UserName);
                p.Add("firstName", obj.FirstName);
                p.Add("lastName", obj.LastName);
                p.Add("email", obj.Email);
                p.Add("password", obj.Password);
                p.Add("salt", obj.Salt);
                p.Add("dateCreated", obj.DateCreated);
                p.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                sqlConnection.Execute("user_insert", p, commandType: CommandType.StoredProcedure);
                obj.Id = p.Get<int>("id");
            }

            SaveUserRoles(obj);
        }

        public void Update(User obj)
        {
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();

                var p = new DynamicParameters();
                p.Add("id", obj.Id);
                p.Add("firstName", obj.FirstName);
                p.Add("lastName", obj.LastName);
                p.Add("email", obj.Email);
                p.Add("password", obj.Password);
                p.Add("salt", obj.Salt);
                sqlConnection.Execute("user_update", p, commandType: CommandType.StoredProcedure);
            }

            SaveUserRoles(obj);
        }

        public void Delete(User obj)
        {
            var sql = "exec user_delete @id";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                sqlConnection.Execute(sql, new { id = obj.Id });
            }
        }

        public User GetByUserName(string userName)
        {
            User user;
            var sql = "exec user_getByUserName @userName";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                user = sqlConnection.QuerySingleOrDefaultAsync<User>(sql, new { userName }).Result;
            }

            if (user != null)
                user.Roles = GetUserRoles(user.Id);

            return user;
        }

        private void SaveUserRoles(User obj)
        {
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                //delete
                sqlConnection.Execute("exec user_deleteRoles @userId", new { userId = obj.Id });
                //then insert
                foreach (var role in obj.Roles)
                {
                    var p = new DynamicParameters();
                    p.Add("userId", obj.Id);
                    p.Add("role", role);
                    sqlConnection.Execute("user_addRole", p, commandType: CommandType.StoredProcedure);
                }  
            }

        }

        private string[] GetUserRoles(int userId)
        {
            string[] roles;
            var sql = "exec user_getRoles @userId";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                roles = sqlConnection.QueryAsync<string>(sql, new { userId }).Result.ToArray();
            }
            return roles;
        }
    }
}
