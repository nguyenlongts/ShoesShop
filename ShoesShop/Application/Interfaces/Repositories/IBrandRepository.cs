using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Repositories
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {

        //Task<bool> AddAsync(Brand brand);
        //Task UpdateAsync(string newName, string oldName);

        //Task<List<Brand>> GetAllAsync();


        Task<bool> UpdateStatusAsync(int brandId);

    }
}
