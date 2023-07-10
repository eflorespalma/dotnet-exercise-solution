using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise.Repository.Core
{
    public interface IDbService
    {
        Task<IEnumerable<T>> QueryAsync<T>(string StoredProcedure);
        Task<IEnumerable<T>> QueryAsync<T>(string StoredProcedure, DynamicParameters parameters);
        Task<int> ExecuteAsync(string StoredProcedure, DynamicParameters parameters);
        Task<T> ExecuteScalarAsync<T>(string StoredProcedure, DynamicParameters parameters);
        Task<T> QueryFirstOrDefaultAsync<T>(string command, DynamicParameters parameters);
    }
}
