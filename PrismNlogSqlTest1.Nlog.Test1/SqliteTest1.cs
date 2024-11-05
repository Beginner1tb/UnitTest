﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismNlogSqlTest1.Services1.Interfaces;
using PrismNlogSqlTest1.Services1;
using PrismNlogSqlTest1.Services1.Repositories;
using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace PrismNlogSqlTest1.Nlog.Test1
{
    #region 内存数据库连接错误写法
    //public class DatabaseHelper
    //{
    //    private readonly string _connectionString;

    //    public DatabaseHelper(string connectionString)
    //    {
    //        _connectionString = connectionString;
    //    }

    //    public void PrintUsersTableData()
    //    {
    //        //内存数据库错误写法，new的新连接对象会销毁
    //        using var connection = new SQLiteConnection(_connectionString);
    //        connection.Open();

    //        using var cmd = new SQLiteCommand("SELECT * FROM users", connection);
    //        using var reader = cmd.ExecuteReader();

    //        Debug.WriteLine("Current data in users table:");
    //        while (reader.Read())
    //        {
    //            var username = reader.GetString(0);
    //            var role = reader.GetInt32(1);
    //            Debug.WriteLine($"User: {username}, Role: {role}");
    //        }
    //    }
    //}
    #endregion

    //Sqlite内存数据库测试,实现IDispose接口用于释放资源
    public class SqliteTest1:IDisposable
    {
        private  ISqlRepositories _sqlRepositories;
        private readonly SQLiteConnection _sharedConnection;
        private IDbConnection _dbConnection;
        private readonly SQLiteConnectionFactory sqliteFactory;

        public SqliteTest1()
        {
            //// 创建并打开一个共享的内存数据库连接
            //_sharedConnection = new SQLiteConnection("Data Source=:memory:");

            //_sharedConnection.Open();

            //// 创建表
            //using (var cmd = new SQLiteCommand("CREATE TABLE users (user_name TEXT PRIMARY KEY, role INTEGER)", _sharedConnection))
            //{
            //    cmd.ExecuteNonQuery();
            //}

            //// 插入测试数据
            //using (var cmd = new SQLiteCommand("INSERT INTO users (user_name, role) VALUES (@user_name, @role)", _sharedConnection))
            //{
            //    cmd.Parameters.AddWithValue("@user_name", "testUser");
            //    cmd.Parameters.AddWithValue("@role", 1);
            //    cmd.ExecuteNonQuery();
            //}



            //_sqlRepositories = new SqlRepositories(_sharedConnection.ConnectionString); // 初始化

            //var connectionString = "Data Source=:memory:"; // SQLite 内存数据库
            // _dbConnection=new SQLiteConnection(connectionString);
            // sqliteFactory = new SQLiteConnectionFactory(connectionString);

            ////_sharedConnection = (SQLiteConnection)sqliteFactory.CreateConnection();
            //_dbConnection.Open();

            //// 创建测试表并插入数据
            //using (var command = _dbConnection.CreateCommand())
            //{
            //    command.CommandText = "CREATE TABLE users (user_name TEXT, role INTEGER);";
            //    command.ExecuteNonQuery();

            //    command.CommandText = "INSERT INTO users (user_name, role) VALUES ('test_user', 1);";
            //    command.ExecuteNonQuery();
            //}

            //_sqlRepositories = new SqlRepositories(sqliteFactory);

        }

        public void PrintUsersTableData(SQLiteConnection sqliteConnection)
        {
            using var cmd = new SQLiteCommand("SELECT * FROM users", sqliteConnection);
            using var reader = cmd.ExecuteReader();

            Debug.WriteLine("Current data in users table:");
            while (reader.Read())
            {
                var username = reader.GetString(0);
                var role = reader.GetInt32(1);
                Debug.WriteLine($"User: {username}, Role: {role}");
            }
        }
        [Fact]
        public void GetPriorityNum_ReturnsCorrectValue_WithSQLite()
        {
            // Arrange
            var connectionString = "Data Source=:memory:";
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            // 初始化数据库
            InitializeDatabase(connection);

            PrintUsersTableData(connection);
            // 使用现有的连接创建工厂
            var connectionFactory = new TestConnectionFactory(connection);
            var sqlRepositories = new SqlRepositories(connectionFactory);

            // Act
            var result = sqlRepositories.GetPriorityNum("test_user");

            // Assert
            Assert.Equal(1, result);
        }

        // 创建一个用于测试的连接工厂
        public class TestConnectionFactory : IDbConnectionFactory
        {
            private readonly IDbConnection _connection;

            public TestConnectionFactory(IDbConnection connection)
            {
                _connection = connection;
            }

            public IDbConnection CreateConnection()
            {
                return _connection; // 返回同一连接实例
            }

            public IDbDataParameter CreateParameter(string name, object value)
            {
                var parameter = new SQLiteParameter();
                parameter.ParameterName = name;
                parameter.Value = value;
                return parameter;
            }
        }

        private void InitializeDatabase(IDbConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE users (user_name TEXT, role INTEGER);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO users (user_name, role) VALUES ('test_user', 1);";
                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            _sharedConnection?.Dispose();
        }
    }
}