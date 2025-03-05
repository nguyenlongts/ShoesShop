using ShoesShop.Application.DTOs;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<ProductResponseDTO> GetProductsAdmin(int pageSize,int pageNum);
        Task<IEnumerable<Product>> GetProductsCustomerAsync();

        Task<bool> CreateAsync(CreateProductDTO product);
        Task<Product> GetProductByNameAsync(string name);

        Task<GetProductDTO> GetProductByIdAsync(int id);

        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
