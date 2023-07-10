using Exercise.BizLogic.ViewModels.Product;
using System.Threading.Tasks;

namespace Exercise.BizLogic.Products
{
    public interface IProductBizLogic
    {
        Task<int> CreateProduct(CreateProductModel model);
        Task<int> UpdateProduct(UpdateProductModel model);
        Task<int> DeleteProduct(DeleteProductModel model);
    }
}
