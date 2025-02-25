using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Domain.Entities;

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
            await _brandRepository.AddAsync(brand);
            var exists = await _brandRepository.GetByIdAsync(brand.BrandID);
            if (exists != null)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync(int pageSize,int pageNum)
        {
            return await _brandRepository.GetAllAsync(pageNum, pageSize);
        }

        Task<bool> IBrandService.UpdateStatusAsync(int brandID)
        {
            var result = _brandRepository.UpdateStatusAsync(brandID);
            return result;
        }
    }
}
