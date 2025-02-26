using API_ShoesShop.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Infrastructure.Repositories.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDBContext _context;

        public CategoryRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Category> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task AddAsync(CreateCateDTO model)
        {
            var newCate = new Category { Name = model.Name,IsActive=model.IsActive };
            _context.Categories.Add(newCate);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task UpdateAsync(Category Category)
        {
            _context.Update(Category);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateStatusAsync(int CategoryId)
        {
            var Category = await _context.Categories.FindAsync(CategoryId);
            if (Category == null) return false;

            Category.IsActive = !Category.IsActive;
            _context.Categories.Update(Category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Categories.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
        }

        async Task<Category> IGenericRepository<Category>.GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        
        
    }
}
