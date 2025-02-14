using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();

        Task<bool> CreateBrandAsync(Brand brand);

        Task<bool> UpdateStatusAsync(Guid brandID, int newStatus);
    }
}
