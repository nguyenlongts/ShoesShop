using API_ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Repositories
{
    public interface IUserRepository 
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync(int pageNumber, int pageSize);
        Task<ApplicationUser> GetByIdAsync(Guid id);

        Task<bool> UpdateAsync(ApplicationUser entity);
        Task<bool> DeleteAsync(Guid id);

        Task<(bool success, string message)> RegisterAsync(ApplicationUser user, string password);
        Task<bool> UpdateStatusAsync(Guid id);
    }
}
