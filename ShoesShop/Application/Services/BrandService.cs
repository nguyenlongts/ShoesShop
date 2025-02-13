using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Interfaces;
using ShoesShop.Domain.Entities;
using ShoesShop.Infrastructure.Repositories.Interfaces;

namespace ShoesShop.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<bool> CreateBrandAsync(Brand brand)
        {
            return await _brandRepository.AddAsync(brand);
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await _brandRepository.GetAllAsync();
        }
        public async Task<bool> UpdateStatusAsync([FromBody] Guid brandId, int newStatus)
        {
            return await _brandRepository.UpdateStatusAsync(brandId, newStatus);
        }
    }
}
