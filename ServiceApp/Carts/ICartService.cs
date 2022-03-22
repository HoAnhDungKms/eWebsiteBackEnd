using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Cart;
using ViewModel.Common;

namespace ServiceApp.Carts
{
    public interface ICartService
    {
        Task<ApiResult<bool>> Add(CartRequest request);
        Task<List<CartViewModel>> GetCart();
        Task<ApiResult<bool>> UpdateCart(UpdateCartRequest request);
        Task<ApiResult<bool>> DeleteCart(DeleteCartRequets requets);
    }
}
