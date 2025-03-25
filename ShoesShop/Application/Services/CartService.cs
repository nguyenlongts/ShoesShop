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
            return await _cartRepository.AddToCartAsync(request.UserId.ToString(), request.ProductDetailId, request.Quantity);
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            return await _cartRepository.ClearCartAsync(userId);
        }

        public Task<bool> CreateAsync(string userId)
        {
            return _cartRepository.CreateAsync(userId);
        }

        public async Task<IEnumerable<CartItemDTO>> GetAllCartItem(string userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            var cartItems = await _cartRepository.GetAllCartItem(cart.CartId.ToString());
            return cartItems;
        }

        public async Task<Cart?> GetByUserIdAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            return cart;
        }

        public async Task<decimal> GetTotalPriceAsync(string userId)
        {
            var cartItems = await GetAllCartItem(userId);
            return cartItems.Sum(item => item.Price * item.Quantity);
        }

        public async Task<bool> RemoveItemAsync(string userId, int productDetailId)
        {
            return await _cartRepository.RemoveFromCartAsync(userId, productDetailId);
        }

        public async Task<bool> UpdateQuantityAsync(string userId, int cartItemId, int newQuantity)
        {
            return await _cartRepository.UpdateQuantityAsync(userId.ToString(), cartItemId, newQuantity);
        }
    }
}
