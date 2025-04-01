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

        async Task<GetProductDTO> IProductService.GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        async Task<Product> IProductService.GetProductByNameAsync(string name)
        {
            return await _productRepository.GetProductByNameAsync(name);
        }

        async Task<ProductResponseDTO> IProductService.GetProductsCustomerAsync(int pageSize, int pageNum)
        {
            return await _productRepository.GetProductsCustomerAsync(pageSize,pageNum);
        }

        async Task<bool> IProductService.UpdateAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<ProductResponseDTO> FilterProducts(List<int>? brandIds, List<int>? sizeIds, List<int>? colorIds, string? priceRange, int page = 1, int pageSize = 10)
        {
            var result = await _productRepository.GetFilteredProducts(brandIds,sizeIds,colorIds,priceRange,page,pageSize);

            return (result);
        }
    }
}
