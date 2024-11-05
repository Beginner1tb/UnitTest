using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using PrismNlogSqlTest1.Services1.Interfaces;
using System.Data.SQLite;

namespace PrismNlogSqlTest1.Services1.Repositories
{
    public class PostgreSqlConnectionFactory:IDbConnectionFactory
    {
        private readonly string _connectionString;

        public PostgreSqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public IDbDataParameter CreateParameter(string name, object value)
        {
            var parameter= new NpgsqlParameter(name, value);
            return parameter;
        }
    }

    public class SQLiteConnectionFactory:IDbConnectionFactory
    {
        private readonly string _connectionString;
        public SQLiteConnectionFactory(string connectionString) 
        { 
            _connectionString = connectionString; 
        }
        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public IDbDataParameter CreateParameter(string name, object value)
        {
            var parameter=new SQLiteParameter(name, value);
            return parameter;
        }
    }
}
