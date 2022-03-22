using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Cart;
using ViewModel.Common;

namespace ServiceApp.Carts
{
    public class CartService : ICartService
    {
        private readonly eDbContext _context;
        public CartService(eDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> Add(CartRequest request)
        {
            if (request == null)
                return new ApiErrorResult<bool>("Cart can't null");
            foreach (var item in request.CartItems)
            {
                var cart = new Cart();
                cart.ProductId = item.ProductId;
                cart.Price = item.Price;
                cart.Quantity = item.Quantity;
                _context.Add(cart);
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
        public async Task<List<CartViewModel>> GetCart()
        {
            var cart = await _context.Carts.Select(r => new Itemvm()
            {
                Id = r.Id,
                ProductId = r.ProductId,
                Price = r.Price,
                Quantity = r.Quantity,
            }).ToListAsync();

            var cartitem = new List<CartViewModel>();
            foreach (var item in cart)
            {
                var product = _context.Products.FirstOrDefault(u => u.Id == item.ProductId);
                var vm = new CartViewModel()
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Name = product.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Description = product.Decription,
                    Image = product.ImagePath,
                };
                cartitem.Add(vm);

            }
            return cartitem;
        }
        public async Task<ApiResult<bool>> UpdateCart(UpdateCartRequest request)
        {
            if (request == null)
                return new ApiErrorResult<bool>("Request can't null");
            var cart = await _context.Carts.Select(r => new Itemvm()
            {
                Id = r.Id,
                ProductId = r.ProductId,
                Price = r.Price,
                Quantity = r.Quantity,
            }).ToListAsync();
            foreach (var item in cart)
            {
                if (item.ProductId == request.productId)
                {
                    item.Quantity = request.Quantity;
                }

            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
        public async Task<ApiResult<bool>> DeleteCart(DeleteCartRequets requets)
        {
            var carts = new List<ItemViewModel>();
            var addcart = new Cart();
            var cartss = await _context.Carts.FindAsync(requets.Id);
            var cart = await _context.Carts.Select(r => new ItemViewModel()
            {
                ProductId = r.ProductId,
                Price = r.Price,
                Quantity = r.Quantity,
            }).ToListAsync();
            foreach (var item in cart)
            {
                if (item.ProductId != requets.productId)
                {
                    carts.Add(item);
                }

            }
            _context.Carts.Remove(cartss);
            foreach (var item in carts)
            {
                addcart.ProductId = item.ProductId;
                addcart.Price = item.Price;
                addcart.Quantity = item.Quantity;
                _context.Add(addcart);
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
}
}
