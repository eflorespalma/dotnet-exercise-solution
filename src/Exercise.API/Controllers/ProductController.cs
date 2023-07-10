using Exercise.BizLogic.Products;
using Exercise.BizLogic.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Exercise.API.Controllers
{
    [Authorize]
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBizLogic _productBizLogic;
        private readonly IProductQueries _productQueries;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductBizLogic productBizLogic, IProductQueries productQueries, ILogger<ProductController> logger)
        {
            _productBizLogic = productBizLogic;
            _productQueries = productQueries;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel model)
        {
            var id = await _productBizLogic.CreateProduct(model);

            return CreatedAtAction(nameof(CreateProduct), new { id }, id);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductModel model)
        {
            await _productBizLogic.UpdateProduct(model);

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductModel model)
        {
            await _productBizLogic.DeleteProduct(model);

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetProductByIdModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<GetProductModel>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productQueries.GetProductById(id);
            if (result == null)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetProductModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productQueries.GetAllProducts();

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get-products-public")]
        [ProducesResponseType(typeof(IEnumerable<GetProductModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductsPublic()
        {
            var result = await _productQueries.GetAllProducts();

            return Ok(result);
        }
    }
}
