using Dapper;
using Exercise.Domain;
using Exercise.Repository.Core;
using Exercise.Repository.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace Exercise.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbService _dbService;
        public ProductRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<int> CreateProduct(Product entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", entity.Name, DbType.String);
            parameters.Add("@Description", entity.Description, DbType.String);
            parameters.Add("@Price", entity.Price, DbType.Decimal);
            parameters.Add("@Quantity", entity.Quantity, DbType.Int16);
            parameters.Add("@RegistrationUser", entity.RegistrationUser, DbType.String);
            parameters.Add("@RegistrationDate", entity.RegistrationDate, DbType.DateTime);
            parameters.Add("@Active", entity.Active, DbType.Boolean);
            parameters.Add("@Id", DbType.Int32, null, ParameterDirection.Output);

            await _dbService.ExecuteAsync("[dbo].[RegisterProduct]", parameters);

            return parameters.Get<int>("@Id");
        }
        public async Task<int> UpdateProduct(Product entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", entity.Id, DbType.Int32);
            parameters.Add("@Name", entity.Name, DbType.String);
            parameters.Add("@Description", entity.Description, DbType.String);
            parameters.Add("@Price", entity.Price, DbType.Decimal);
            parameters.Add("@Quantity", entity.Quantity, DbType.Int16);
            parameters.Add("@ModificationUser", entity.ModificationUser, DbType.String);
            parameters.Add("@ModificationDate", entity.ModificationDate, DbType.DateTime);
            parameters.Add("@Active", entity.Active, DbType.Boolean);

            var result = await _dbService.ExecuteAsync("[dbo].[UpdateProduct]", parameters);

            return result;
        }
        public async Task<int> DeleteProduct(Product entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", entity.Id, DbType.Int32);
            parameters.Add("@ModificationUser", entity.ModificationUser, DbType.String);
            parameters.Add("@ModificationDate", entity.ModificationDate, DbType.DateTime);

            var result = await _dbService.ExecuteAsync("[dbo].[DeleteProduct]", parameters);

            return result;
        }
        public async Task<bool> ValidateProductExistence(string name)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", name, DbType.String);

            var result = await _dbService.ExecuteScalarAsync<bool>("[dbo].[ValidateProductExistence]", parameters);

            return result;
        }
        public async Task<Product> GetProductById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            var result = await _dbService.QueryFirstOrDefaultAsync<Product>("[dbo].[GetProductById]", parameters);

            return result;
        }
    }
}
