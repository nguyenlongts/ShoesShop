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

        public Task<Cart?> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
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
