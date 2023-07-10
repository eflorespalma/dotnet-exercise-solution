using Dapper;
using Exercise.Domain;
using Exercise.Repository.Core;
using System.Data;
using System.Threading.Tasks;

namespace Exercise.Repository.Users
{
    public class UserRepository: IUserRepository
    {
        private readonly IDbService _dbService;
        public UserRepository(IDbService dbService)
        {
            _dbService = dbService;
        }
        public async Task<int> CreateUser(User entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", entity.Email, DbType.String);
            parameters.Add("@Password", entity.Password, DbType.String);
            parameters.Add("@RegistrationDate", entity.RegistrationDate, DbType.DateTime);
            parameters.Add("@Active", entity.Active, DbType.Boolean);
            parameters.Add("@Id", DbType.Int32, null, ParameterDirection.Output);

            await _dbService.ExecuteAsync("[dbo].[RegisterUser]", parameters);

            return parameters.Get<int>("@Id");
        }
        public async Task<bool> ValidateUserExistence(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);

            var result = await _dbService.ExecuteScalarAsync<bool>("[dbo].[ValidateUserExistence]", parameters);

            return result;
        }
    }
}
