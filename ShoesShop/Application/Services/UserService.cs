using API_ShoesShop.Domain.Entities;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;

namespace ShoesShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _userRepository.DeleteAsync(id);
        }

        Task<IEnumerable<ApplicationUser>> IUserService.GetAllAsync(int pageSize, int pageNum)
        {
            return _userRepository.GetAllAsync(pageNum, pageSize);
        }

        Task<ApplicationUser> IUserService.GetByIdAsync(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        Task<bool> IUserService.UpdateAsync(ApplicationUser user)
        {
            return _userRepository.UpdateAsync(user);
        }

        Task<bool> IUserService.UpdateStatusAsync(Guid userID)
        {
            return _userRepository.UpdateStatusAsync(userID);
        }
    }
}
