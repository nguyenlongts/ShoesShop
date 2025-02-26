using ShoesShop.Application.DTOs;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllAsync(int pageSize,int pageNum);

        Task<bool> CreateBrandAsync(CreateBrandDTO brand);

        Task<bool> UpdateStatusAsync(int brandID);
    }
}
