using API_ShoesShop.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Infrastructure.Repositories.Implement
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDBContext _context;

        public BrandRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task UpdateAsync(Brand brand)
        {
            _context.Update(brand);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateStatusAsync(int brandId)
        {
            var brand = await _context.Brands.FindAsync(brandId);
            if (brand == null) return false;

            brand.IsActive = !brand.IsActive;
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var existingBrand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(existingBrand);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Brands.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
        }

        async Task<Brand> IGenericRepository<Brand>.GetByIdAsync(int id)
        {
            return await _context.Brands.FindAsync(id);
        }

        
        
    }
}
