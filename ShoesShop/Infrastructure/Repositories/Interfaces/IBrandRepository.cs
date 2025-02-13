
using ShoesShop.Domain.Entities;

namespace ShoesShop.Infrastructure.Repositories.Interfaces
{
    public interface IBrandRepository
    {

        Task<bool> AddAsync(Brand brand);
        Task UpdateAsync(string newName,string oldName);

        Task<List<Brand>> GetAllAsync();


        Task<bool> UpdateStatusAsync(Guid brandId, int newStatus);

    }
}
