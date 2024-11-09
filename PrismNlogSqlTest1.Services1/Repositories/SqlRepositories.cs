using Npgsql;
using System;
using PrismNlogSqlTest1.Services1.Interfaces;


namespace PrismNlogSqlTest1.Services1.Repositories
{
    public class SqlRepositories : ISqlRepositories
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public SqlRepositories(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public int GetPriorityNum(string username)
        {
            var priorityNum = 999;
            //using var conn = new NpgsqlConnection(_connectionString);

            //var query = "select role from users where user_name=@username";
            //conn.Open();
            //using var cmd = new NpgsqlCommand(query, conn);
            //cmd.Parameters.AddWithValue($"@username", username);
            //var reader = cmd.ExecuteReader();

            using var conn = _connectionFactory.CreateConnection();

            var query = "select role from users where user_name=@username";
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            using var cmd = conn.CreateCommand();
            cmd.CommandText = query;

            var parameter = _connectionFactory.CreateParameter("@username", username);
            cmd.Parameters.Add(parameter);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                priorityNum = reader.GetInt32(0);
            }

            return priorityNum;
        }
    }
}
