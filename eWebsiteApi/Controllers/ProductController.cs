using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Products;
using System.Threading.Tasks;
using ViewModel.Product;

namespace eWebsiteApi.Controllers
{
    //api/products
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(
            IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var products = await _productService.GetById(productId);
            if (products == null)
                return BadRequest("Can't find product");
            return Ok(products);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _productService.CreateProduct(request);
            if (!product.IsSuccessed)
                return BadRequest("Can't create product");
            return Ok(product);
        }

        [HttpPut("{productId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromForm] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = productId;
            var affectedResult = await _productService.UpdateProduct(request);
            if (!affectedResult.IsSuccessed)
                return BadRequest("Can't update product");
            return Ok(affectedResult);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _productService.DeleteProduct(productId);
            if (!affectedResult.IsSuccessed)
                return BadRequest("Can't delete product");
            return Ok();
        }


        [HttpPut("{productId}/productCategories")]
        public async Task<IActionResult> CategoryAssign(int productId, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.CategoryAssign(productId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest("Can't assign category");
            }
            return Ok(result);
        }
    }
}
