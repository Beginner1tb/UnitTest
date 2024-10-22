using System;
using System.Collections.Generic;
using System.Text;
using WpfInterfaceSqlTest.Interfaces;
using WpfInterfaceSqlTest.Models;
using Npgsql;
using System.Collections.ObjectModel;

namespace WpfInterfaceSqlTest.Repositories
{
    public class UserRepositories:IUserRepositories
    {
        private readonly string _connectionString;

        public UserRepositories(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            var query = "insert into users(id,user_name,password,role) values (@Id,@Username,@Password,@Role";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Role", user.Role);

            cmd.ExecuteNonQuery();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = new ObservableCollection<User>();
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            var query = "SELECT id, user_name, role FROM users";
            var cmd = new NpgsqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var user = new User()
                {
                    Id = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    Role = reader.GetInt32(2),
                    Password = null
                };
                users.Add(user);
            }
            return users;
        }

        public int GetPriorityNum(string username)
        {
            var priorityNum = 999;
            using var conn=new NpgsqlConnection(_connectionString);
            var query = "select role from users where user_name=@username";
            conn.Open();
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue($"@username", username);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                priorityNum = reader.GetInt32(0);
            }

            return priorityNum;
        }

        public User GetUserInfo(string username)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
