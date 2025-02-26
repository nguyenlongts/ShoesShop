using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Domain.Entities;
using ShoesShop.Infrastructure.Repositories.Implement;

namespace ShoesShop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryService(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public async Task<bool> CreateCategoryAsync(CreateCateDTO model)
        {
            await _CategoryRepository.AddAsync(model);
            var exists = await _CategoryRepository.GetByNameAsync(model.Name);
            if (exists != null)
            {
                return true;
            }
            return false;
        }
        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var cate = await _CategoryRepository.GetByNameAsync(name);
            return cate;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(int pageSize,int pageNum)
        {
            return await _CategoryRepository.GetAllAsync(pageNum, pageSize);
        }

        Task<bool> ICategoryService.UpdateStatusAsync(int CategoryID)
        {
            var result = _CategoryRepository.UpdateStatusAsync(CategoryID);
            return result;
        }
    }
}
