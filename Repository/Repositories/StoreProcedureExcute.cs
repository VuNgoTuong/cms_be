using Common.Commons;
using Dapper;
using Microsoft.Data.SqlClient;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StoreProcedureExecute : IStoreProcedureExecute
    {
        public string _connectionString { get; set; }

        public StoreProcedureExecute()
        {
            _connectionString = ConfigHelper.Get("ConnectionStrings", "DefaultConnection");
        }

        /// <summary>
        /// Execute store not return any values 
        /// </summary>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters"> Array of parameters </param>
        public async Task ExecuteNotReturn(string storeProcedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                await sql.QueryFirstOrDefaultAsync(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Execute store  return a type
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public T ExecuteReturnScaler<T>(string storeProcedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                return (T)Convert.ChangeType(sql.ExecuteScalar<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
            }
        }

        /// <summary>
        /// Execute store  return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReturnList<T>(string storeProcedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                return await sql.QueryAsync<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Execute store  return single record
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<T> ExecuteReturnSingle<T>(string storeProcedureName, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                return await sql.QueryFirstOrDefaultAsync<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Execute store  return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                return await sql.QueryAsync<T>(query, null, commandType: System.Data.CommandType.Text);
            }
        }
        /// <summary>
        /// Execute store  return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                return await sql.QueryAsync<T>(query, parameters);
            }
        }

        /// <summary>
        /// Execute store  return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<ListResult<T>> ExecuteReturnListResultBySql<T>(string query, DynamicParameters parameters = null)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                ListResult<T> response = new ListResult<T>();
                sql.Open();
                using (var multi = sql.QueryMultiple(query, parameters))
                {
                    var data = multi.Read<T>().ToList();
                    var dataCount = multi.Read<int>().First();

                    response.total = dataCount;
                    response.items = data;
                }
                return response;
            }
        }
    }
}
