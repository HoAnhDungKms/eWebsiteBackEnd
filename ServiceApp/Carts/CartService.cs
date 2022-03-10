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
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            var userid = user.Id;
            foreach (var item in request.CartItems)
            {
                var cart = new Cart();
                cart.ProductId = item.ProductId;
                cart.UserId = userid;
                cart.Price = item.Price;
                cart.Quantity = item.Quantity;
                _context.Add(cart);
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
        public async Task<List<CartViewModel>> GetCartByUserName(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            var cart = await _context.Carts.Select(r => new Itemvm()
            {
                Id = r.Id,
                ProductId = r.ProductId,
                UserId = r.UserId,
                Price = r.Price,
                Quantity = r.Quantity,
            }).ToListAsync();

            var cartitem = new List<CartViewModel>();
            foreach (var item in cart)
            {
                if (item.UserId == user.Id)
                {
                    var product = _context.Products.FirstOrDefault(u => u.Id == item.ProductId);
                    var vm = new CartViewModel()
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        ProductId = item.ProductId,
                        Name = product.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Description = product.Decription,
                        Image = product.ImagePath,
                    };
                    cartitem.Add(vm);
                }
            }
            return cartitem;
        }
        public async Task<ApiResult<bool>> DeleteCart(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            var cart = await _context.Carts.FindAsync(user.Id);
            if(cart == null) 
                return new ApiErrorResult<bool>("Cart doesn't exits in database");
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}
