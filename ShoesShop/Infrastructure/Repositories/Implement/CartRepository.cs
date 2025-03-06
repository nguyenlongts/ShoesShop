using API_ShoesShop.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Infrastructure.Repositories.Implement
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDBContext _context;
        public CartRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToCartAsync(Guid userId, int ProductDetailId, int quantity)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId.ToString());
            var existCartItem =  cart.CartItems.FirstOrDefault(ci=>ci.ProductDetailId==ProductDetailId);
            if (existCartItem != null)
            {
                existCartItem.Quantity += quantity;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductDetailId = ProductDetailId,
                    Quantity = quantity,
                };
                _context.CartItems.Add(newCartItem);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartAsync(Guid userId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId.ToString());
            foreach (var cartItem in cart.CartItems){
                _context.CartItems.Remove(cartItem);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateAsync(Guid userId)
        {
            var existCart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId.ToString());
            if (existCart == null)
            {
                var newCart = new Cart { UserId = userId.ToString() };
                _context.Carts.Add(newCart);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<Cart> GetCartByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCartAsync(Guid userId, int ProductDetailId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId.ToString());
            foreach (var cartItem in cart.CartItems)
            {
                if (cartItem.ProductDetailId == ProductDetailId)
                {
                    _context.CartItems.Remove(cartItem);
                }
            }

            await _context.SaveChangesAsync();
            return true;

        }

        public Task<bool> UpdateQuantityAsync(Guid userId, int ProductDetailId, int newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
