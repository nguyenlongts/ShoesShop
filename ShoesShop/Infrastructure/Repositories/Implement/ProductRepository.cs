using API_ShoesShop.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Infrastructure.Repositories.Implement
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _context;
        public ProductRepository(AppDBContext context)
        {
            _context = context;
        }

        async Task<bool> IProductRepository.CreateAsync(CreateProductDTO product)
        {
            var newProduct = new Product { CateID = product.CateID, BrandID = product.BrandID, Name = product.ProductName, Description = product.Description,BasePrice=product.BasePrice };
            _context.Products.Add(newProduct);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        async Task<bool> IProductRepository.DeleteAsync(int id)
        {
            var existProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (existProduct == null)
            {
                return false;
            }

            _context.Products.Remove(existProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductResponseDTO> GetProductsAdmin(int pageSize, int pageNum)
        {

            int totalProducts = await _context.Products.CountAsync();


            var products = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new GetProductDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    BasePrice = (decimal)p.BasePrice,
                    Image = p.Image,
                    BrandName = p.Brand.Name,
                    CategoryName = p.Category.Name,
                    IsActive = p.IsActive
                }).ToListAsync();
            int totalPages = (int)Math.Ceiling((decimal)totalProducts / pageSize);
            return new ProductResponseDTO
            {
                TotalPages = totalPages,
                TotalProducts = totalProducts,
                Products = products
            };
        }


        async Task<GetProductDTO> IProductRepository.GetProductByIdAsync(int id)
        {
            var existProduct = await _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p=>p.ProductDetails).FirstOrDefaultAsync(p => p.ProductId == id);
            {
                return new GetProductDTO
                {
                    ProductId = existProduct.ProductId,
                    Name = existProduct.Name,
                    Description = existProduct.Description,
                    BasePrice = (decimal)existProduct.BasePrice,
                    Image = existProduct.Image,
                    BrandName = existProduct.Brand.Name,
                    CategoryName = existProduct.Category.Name,
                    IsActive = existProduct.IsActive
                };
            }
            return null;
        }

        async Task<Product> IProductRepository.GetProductByNameAsync(string name)
        {
            return await (_context.Products.FirstOrDefaultAsync(p => p.Name == name));
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductsCustomerAsync()
        {
            return await _context.Products.Where(p => p.IsActive == true).ToListAsync();
        }

        async Task<bool> IProductRepository.UpdateAsync(Product product)
        {
            _context.Update(product);
            var result = await _context.SaveChangesAsync();
            return result > 0;

        }
    }
}
