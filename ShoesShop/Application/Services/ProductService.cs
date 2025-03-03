using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        async Task<bool> IProductService.CreateAsync(CreateProductDTO product)
        {
            return await _productRepository.CreateAsync(product);
        }

        async Task<bool> IProductService.DeleteAsync(int id)
        {
            var result = await _productRepository.DeleteAsync(id);
            return result;
        }

        public async Task<ProductResponseDTO> GetAllAdminAsync(int pageSize, int pageNum)
        {
            return await _productRepository.GetProductsAdmin(pageSize,pageNum);
        }

        async Task<Product> IProductService.GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        async Task<Product> IProductService.GetProductByNameAsync(string name)
        {
            return await _productRepository.GetProductByNameAsync(name);
        }

        async Task<IEnumerable<Product>> IProductService.GetProductsCustomerAsync()
        {
            return await _productRepository.GetProductsCustomerAsync();
        }

        async Task<bool> IProductService.UpdateAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }
    }
}
