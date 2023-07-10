using Exercise.BizLogic.ViewModels.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise.BizLogic.Products
{
    public interface IProductQueries
    {
        Task<IEnumerable<GetProductModel>> GetAllProducts();
        Task<GetProductByIdModel> GetProductById(int id);
    }
}
