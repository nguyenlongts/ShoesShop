using API_ShoesShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Interfaces.Repositories;

namespace ShoesShop.Infrastructure.Repositories.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return false; 
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _userManager.Users.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            return _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<bool> UpdateAsync(ApplicationUser entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        async Task<bool> IUserRepository.UpdateStatusAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return false;
            }
            user.isActive = !user.isActive;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
