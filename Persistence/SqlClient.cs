﻿using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class SqlClient : ISqlClient
    {
        private readonly string _connectionString;

        public SqlClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<int> ExecuteAsync(string sql, object param = null)
        {
            using var connection = new MySqlConnection(_connectionString);

            return connection.ExecuteAsync(sql, param);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            using var connection = new MySqlConnection(_connectionString);

            return connection.QueryAsync<T>(sql, param);
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null)
        {
            using var connection = new MySqlConnection(_connectionString);

            return connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
    }
}
