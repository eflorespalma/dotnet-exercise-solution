using Exercise.BizLogic.ViewModels.Product;
using Exercise.Domain;
using Exercise.Domain.Exceptions;
using Exercise.Repository.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Exercise.BizLogic.Products
{
    public class ProductBizLogic : IProductBizLogic
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<ProductBizLogic> _logger;
        public ProductBizLogic(IUnitofWork unitofWork, ILogger<ProductBizLogic> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<int> CreateProduct(CreateProductModel model)
        {
            int id;
            try
            {
                var nameExists = await _unitofWork._productRepository.ValidateProductExistence(model.Name);
                if (nameExists)
                {
                    //This logger information only make sense in case we need to control existing product attempts.
                    _logger.LogInformation($"Product {model.Name} already exists in database");
                    throw new ExerciseBaseException($"Product {model.Name} already exists in database.");
                }

                var product_entity = new Product(model.Name, model.Description, model.Price, model.Quantity, model.RegistrationUser);

                id = await _unitofWork._productRepository.CreateProduct(product_entity);
                _unitofWork.Commit();
            }
            catch (Exception)
            {
                _unitofWork.RollBack();
                throw;
            }
            return id;
        }

        public async Task<int> UpdateProduct(UpdateProductModel model)
        {
            int result;
            try
            {
                var old_product_entity = await _unitofWork._productRepository.GetProductById(model.Id);

                //only in case product name is different make the validation
                if (!old_product_entity.Name.ToUpper().Equals(model.Name.ToUpper()))
                {
                    var nameExists = await _unitofWork._productRepository.ValidateProductExistence(model.Name);
                    if (nameExists)
                    {
                        _logger.LogInformation($"Product {model.Name} already exists in database");
                        throw new ExerciseBaseException($"Product {model.Name} already exists in database.");
                    }
                }

                var product_entity = new Product(model.Id, model.Name, model.Description, model.Price, model.Quantity, model.ModificationUser, model.Active);

                result = await _unitofWork._productRepository.UpdateProduct(product_entity);
                _unitofWork.Commit();
            }
            catch (Exception)
            {
                _unitofWork.RollBack();
                throw;
            }
            return result;
        }

        public async Task<int> DeleteProduct(DeleteProductModel model)
        {
            int result;

            try
            {
                var product_entity = new Product(model.Id, model.ModificationUser);

                result = await _unitofWork._productRepository.DeleteProduct(product_entity);
                _unitofWork.Commit();
            }
            catch (Exception)
            {
                _unitofWork.RollBack();
                throw;
            }
            return result;
        }
    }
}
