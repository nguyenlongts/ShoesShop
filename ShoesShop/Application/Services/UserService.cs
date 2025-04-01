using API_ShoesShop.Application.DTOs;
using System.Text;
using API_ShoesShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;

namespace ShoesShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartService _cartService;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IUserRepository userRepository, ICartService cartService, IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _cartService = cartService;
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task<(bool success, string message)> RegisterAsync(RegisterDTO model)
        {
            var existUser = await _userManager.FindByEmailAsync(model.Email);
            if (existUser != null)
                return (false, "Email đã tồn tại!");

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Address = model.Address,
                DoB = model.DoB,
                PhoneNumber = model.Phone,
                Gender = model.Gender,
                LastName = model.LastName,
                FirstName = model.FirstName
            };

            var result = await _userRepository.RegisterAsync(user, model.Password);
            if (!result.success)
                return (false, "Đăng ký thất bại!");

            var createCart = await _cartService.CreateAsync(user.Id);
            if (!createCart)
                return (false, "Không thể tạo giỏ hàng!");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var confirmLink = $"http://localhost:5258/api/Auth/confirm-email?userId={user.Id}&token={encodedToken}";

            string subject = "Xác nhận tài khoản";
            string body = $"<p>Nhấp vào link sau để xác nhận tài khoản: <a href='{confirmLink}'>Xác nhận Email</a></p>";

            await _emailService.SendMailAsync(user.Email, subject, body);

            return (true, "Đăng ký thành công! Vui lòng kiểm tra email để xác nhận.");
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _userRepository.DeleteAsync(id);
        }

        

        Task<IEnumerable<ApplicationUser>> IUserService.GetAllAsync(int pageSize, int pageNum)
        {
            return _userRepository.GetAllAsync(pageNum, pageSize);
        }

        Task<UserInfoResponse> IUserService.GetByIdAsync(Guid id)
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
