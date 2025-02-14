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

        public async Task<bool> AddAsync(Brand brand)
        {
            if (await _context.Brands.AnyAsync(b => b.Name == brand.Name))
                return false;
            if (brand.BrandID == Guid.Empty) 
            {
                brand.BrandID = Guid.NewGuid(); 
            }
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task UpdateAsync(string newName,string oldName)
        {
            var existBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == oldName);
            if (existBrand != null)
            {
                existBrand.Name = newName;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateStatusAsync(Guid brandId, int newStatus)
        {
            var brand = await _context.Brands.FindAsync(brandId);
            if (brand == null) return false;

            brand.Status = newStatus;
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
