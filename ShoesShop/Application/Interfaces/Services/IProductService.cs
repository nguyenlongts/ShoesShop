using ShoesShop.Application.DTOs;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductResponseDTO> GetAllAdminAsync(int pageSize,int pageNum);
        Task<IEnumerable<Product>> GetProductsCustomerAsync();
        Task<bool> CreateAsync(CreateProductDTO product);
        Task<Product> GetProductByNameAsync(string name);

        Task<Product> GetProductByIdAsync(int id);

        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
