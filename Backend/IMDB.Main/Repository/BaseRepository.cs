
using Assignment.Models.DBModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Assignment3.Repository
{
    public class BaseRepository<T> where T : class
    {
        private readonly string _connectionString;
        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> Get(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<T>(query);
        }

        public IEnumerable<T> Get(string query,object parameter)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<T>(query,parameter);
        }

        public T GetSingle(string query, object parameter)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QuerySingleOrDefault<T>(query,parameter);
        }
   
        public U ExecuteStoredProcedure<U>(string nameOfProcedure, object parameter)
        {
            using var connection = new SqlConnection(_connectionString);

            // Use DynamicParameters to handle both input and output parameters
            var dynamicParameters = new DynamicParameters(parameter);
            dynamicParameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure using Query method
            connection.Execute(nameOfProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);

            // Retrieve the value of the output parameter
            var outputParameter = dynamicParameters.Get<int>("@Result");

            // Return the value of the output parameter
            return (U)Convert.ChangeType(outputParameter, typeof(U));
        }





        public int Create(string query, object parameter)
        {
            using var connection = new SqlConnection(_connectionString);

            query += ";SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = connection.QuerySingleOrDefault<int>(query, parameter);

            if(id == null)
            {
                return -1;
            }
            return id;
        }

        public int Delete(string query, object parameter)
        {
            using var connection = new SqlConnection(_connectionString);

            var rowAffected = connection.Execute(query, parameter);

            if (rowAffected > 0)
                return rowAffected;
   
                return -1;
        }

        public int Update(string query, object parameter)
        {
            using var connection = new SqlConnection(_connectionString);

            var rowAffected = connection.Execute(query, parameter);

            if (rowAffected > 0)
                return rowAffected;

            return -1;
        }

    }
}
