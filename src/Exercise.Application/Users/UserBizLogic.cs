using Exercise.BizLogic.ViewModels.User;
using Exercise.Domain;
using Exercise.Domain.Exceptions;
using Exercise.Repository.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Exercise.BizLogic.Users
{
    public class UserBizLogic : IUserBizLogic
    {
        private readonly IUnitofWork _unitofWork;
        public UserBizLogic(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public async Task<int> CreateUser(CreateUserModel model)
        {
            int id;
            try
            {
                var emailExists = await _unitofWork._userRepository.ValidateUserExistence(model.Email);
                if (emailExists)
                {
                    throw new ExerciseBaseException($"Email {model.Email} already exists in database.");
                }

                var password_hash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                var user_entity = new User(model.Email, password_hash);

                id = await _unitofWork._userRepository.CreateUser(user_entity);
                _unitofWork.Commit();
            }
            catch (Exception)
            {
                _unitofWork.RollBack();
                throw;
            }

            return id;
        }
    }
}
