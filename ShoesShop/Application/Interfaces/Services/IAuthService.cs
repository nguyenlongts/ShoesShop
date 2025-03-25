using API_ShoesShop.Application.DTOs;
using API_ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<(bool success, string message)> RegisterAsync(RegisterDTO model);
        Task<(bool success, string token, string message)> LoginAsync(LoginDTO model);
        Task<bool> ConfirmEmailAsync(string userId, string token);
    }
}
