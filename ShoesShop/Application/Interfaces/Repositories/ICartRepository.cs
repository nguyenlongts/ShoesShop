using ShoesShop.Application.DTOs;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<bool> CreateAsync(Guid userId);

        Task<Cart> GetCartByUserId(Guid userId);
        Task<bool> AddToCartAsync(Guid userId, int ProductDetailId, int quantity);
        Task<bool> RemoveFromCartAsync(Guid userId, int ProductDetailId);

        Task<bool> UpdateQuantityAsync(Guid userId, int cartItemId, int newQuantity);

        Task<bool> ClearCartAsync(Guid userId);

        Task<IEnumerable<CartItemDTO>> GetAllCartItem(Guid cartId);

    }
}
