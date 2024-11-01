using Npgsql;
using System;
using PrismNlogSqlTest1.Core1.Interfaces;


namespace PrismNlogSqlTest1.Core1.Repositories
{
    public class SqlRepositories : ISqlRepositories
    {
        private readonly string _connectionString;
        public SqlRepositories(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int GetPriorityNum(string username)
        {
            var priorityNum = 999;
            using var conn = new NpgsqlConnection(_connectionString);
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
    }
}
