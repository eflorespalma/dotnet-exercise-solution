using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Exercise.Repository.Core
{
    //Implemented to facilitate unit testing of repositories
    public class DbService : IDbService
    {
        private SqlConnection _sqlConnection;
        private IDbTransaction _dbTransaction;
        public DbService(SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string StoredProcedure)
        {
            var result = await _sqlConnection.QueryAsync<T>(StoredProcedure, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string StoredProcedure, DynamicParameters parameters)
        {
            var result = await _sqlConnection.QueryAsync<T>(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ExecuteAsync(string StoredProcedure, DynamicParameters parameters)
        {
            var result = await _sqlConnection.ExecuteAsync(StoredProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction);
            return result;
        }

        public async Task<T> ExecuteScalarAsync<T>(string StoredProcedure, DynamicParameters parameters)
        {
            var result = await _sqlConnection.ExecuteScalarAsync<T>(StoredProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction);
            return result;
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string command, DynamicParameters parameters)
        {
            return await _sqlConnection.QueryFirstOrDefaultAsync<T>(command, param: parameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction);
        }
    }
}
