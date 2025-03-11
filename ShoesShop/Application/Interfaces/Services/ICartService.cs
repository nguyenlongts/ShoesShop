using ShoesShop.Application.DTOs;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<bool> CreateAsync(Guid userId);
        Task<Cart?> GetByUserIdAsync(Guid userId);
        Task<bool> AddItemAsync(AddToCartRequest request);
        Task<bool> UpdateQuantityAsync(Guid userId, int productDetailId, int newQuantity);
        Task<bool> RemoveItemAsync(Guid userId, int productDetailId);
        Task<bool> ClearCartAsync(Guid userId);
        Task<decimal> GetTotalPriceAsync(Guid userId);

        Task<IEnumerable<CartItemDTO>> GetAllCartItem(Guid userId);
    }
}
