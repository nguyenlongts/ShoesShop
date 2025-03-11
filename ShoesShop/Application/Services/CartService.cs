using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddItemAsync(AddToCartRequest request)
        {
            return await _cartRepository.AddToCartAsync(request.UserId, request.ProductDetailId, request.Quantity);
        }

        public Task<bool> ClearCartAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync(Guid userId)
        {
            return _cartRepository.CreateAsync(userId);
        }

        public async Task<IEnumerable<CartItemDTO>> GetAllCartItem(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            var cartItems = await _cartRepository.GetAllCartItem(cart.CartId);
            return cartItems;
        }

        public async Task<Cart?> GetByUserIdAsync(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            return cart;
        }

        public Task<decimal> GetTotalPriceAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveItemAsync(Guid userId, int productDetailId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateQuantityAsync(Guid userId, int productDetailId, int newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
