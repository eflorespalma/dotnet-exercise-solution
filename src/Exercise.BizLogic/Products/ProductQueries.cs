using Dapper;
using Exercise.BizLogic.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Exercise.BizLogic.Products
{
    public class ProductQueries: IProductQueries
    {
        private string _connectionString = string.Empty;

        public ProductQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<GetProductModel>> GetAllProducts()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.QueryAsync<GetProductModel>("[dbo].[GetProducts]", commandType: CommandType.StoredProcedure);
        }

        public async Task<GetProductByIdModel> GetProductById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            return await connection.QueryFirstOrDefaultAsync<GetProductByIdModel>("[dbo].[GetProductById]", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
