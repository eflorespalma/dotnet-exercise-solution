using Exercise.Repository.Interfaces;
using Exercise.Repository.Users;

namespace Exercise.Repository.UnitOfWork
{
    public interface IUnitofWork
    {
        public IProductRepository _productRepository { get; }
        public IUserRepository _userRepository { get; }
        void RollBack();
        void Commit();
    }
}
