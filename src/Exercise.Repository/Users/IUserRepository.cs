using Exercise.Domain;
using System.Threading.Tasks;

namespace Exercise.Repository.Users
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User entity);
        Task<bool> ValidateUserExistence(string email);
    }
}
