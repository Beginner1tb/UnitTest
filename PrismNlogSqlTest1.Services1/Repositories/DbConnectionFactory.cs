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
        private IDbConnection _sharedConnection;  // 用于在测试环境中共享连接
        public SQLiteConnectionFactory(string connectionString) 
        { 
            _connectionString = connectionString; 
        }
        public IDbConnection CreateConnection()
        {
            // 如果_sharedConnection为空，或连接已经关闭，则创建新的连接
            if (_sharedConnection == null || _sharedConnection.State == ConnectionState.Closed)
            {
                _sharedConnection = new SQLiteConnection(_connectionString);
                _sharedConnection.Open();  // 保持连接在整个测试中有效
            }
            return _sharedConnection;
        }

        public IDbDataParameter CreateParameter(string name, object value)
        {
            var parameter=new SQLiteParameter(name, value);
            return parameter;
        }
    }
}
