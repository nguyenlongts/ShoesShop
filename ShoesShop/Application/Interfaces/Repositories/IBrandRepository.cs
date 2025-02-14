using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Repositories
{
    public interface IBrandRepository
    {

        Task<bool> AddAsync(Brand brand);
        Task UpdateAsync(string newName, string oldName);

        Task<List<Brand>> GetAllAsync();


        Task<bool> UpdateStatusAsync(Guid brandId, int newStatus);

    }
}
