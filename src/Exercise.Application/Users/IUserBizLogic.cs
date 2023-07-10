using Exercise.BizLogic.ViewModels.User;
using System.Threading.Tasks;

namespace Exercise.BizLogic.Users
{
    public interface IUserBizLogic
    {
        Task<int> CreateUser(CreateUserModel model);
    }
}
