using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllAsync(int pageSize,int pageNum);

        Task<bool> CreateBrandAsync(Brand brand);

        Task<bool> UpdateStatusAsync(int brandID);
    }
}
