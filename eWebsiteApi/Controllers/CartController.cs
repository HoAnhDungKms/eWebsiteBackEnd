using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Carts;
using System.Threading.Tasks;
using ViewModel.Cart;

namespace eWebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(
            ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carts = await _cartService.GetCart();
            return Ok(carts);
        }
        [HttpPut("{cartproductId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateCart([FromForm] UpdateCartRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _cartService.UpdateCart(request);
            if (!result.IsSuccessed)
                return BadRequest("Can't update Cart");
            return Ok(result);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> CreateCart([FromForm] CartRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _cartService.Add(request);
            if (!result.IsSuccessed)
                return BadRequest("Can't create Cart");
            return Ok(result);
        }
        [HttpDelete("{productid}")]
        public async Task<IActionResult> DeleteProductFromCart(DeleteCartRequets requets)
        {
            var result = await _cartService.DeleteCart(requets);
            if (!result.IsSuccessed)
                return BadRequest("Can't delete product in cart");
            return Ok(result);
        }
    }
}
