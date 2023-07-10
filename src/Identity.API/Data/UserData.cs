using Dapper;
using Exercise.Domain;
using System.Data;
using System.Data.SqlClient;

namespace Identity.API.Data
{
    public class UserData : IUserData
    {
        private string _connectionString = string.Empty;

        public UserData(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        public async Task<User> GetUserByEmail(string Email)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var parameters = new DynamicParameters();
            parameters.Add("@Email", Email, DbType.String);

            return await connection.QueryFirstOrDefaultAsync<User>("[dbo].[GetUserByAccount]", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public interface IUserData
    {
        Task<User> GetUserByEmail(string Email);
    }
}
