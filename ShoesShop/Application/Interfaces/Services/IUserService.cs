using API_ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync(int pageSize,int pageNum);

        Task<ApplicationUser> GetByIdAsync(Guid id);

        Task<bool> UpdateAsync(ApplicationUser user);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> UpdateStatusAsync(Guid userID);
    }
}
