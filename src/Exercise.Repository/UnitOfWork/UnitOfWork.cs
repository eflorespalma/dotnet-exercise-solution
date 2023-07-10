using Exercise.Repository.Interfaces;
using Exercise.Repository.Users;
using System;
using System.Data;

namespace Exercise.Repository.UnitOfWork
{
    public class UnitofWork : IUnitofWork, IDisposable
    {
        public IProductRepository _productRepository { get; }
        public IUserRepository _userRepository { get; }

        IDbTransaction _dbTransaction;

        public UnitofWork(IDbTransaction dbTransaction, IProductRepository productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _dbTransaction = dbTransaction;
        }
        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void RollBack()
        {
            _dbTransaction.Rollback();
        }

        public void Dispose()
        {
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}
