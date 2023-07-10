using Exercise.Domain;
using System.Threading.Tasks;

namespace Exercise.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<int> CreateProduct(Product entity);
        Task<int> UpdateProduct(Product entity);
        Task<int> DeleteProduct(Product entity);
        Task<bool> ValidateProductExistence(string name);
        Task<Product> GetProductById(int id);
    }
}
