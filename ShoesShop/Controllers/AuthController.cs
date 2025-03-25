using API_ShoesShop.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Interfaces.Services;

namespace API_ShoesShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var (success, message) = await _authService.RegisterAsync(model);
            if (!success)
                return BadRequest(new { message });

            return Ok(new { message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var (success, token, message) = await _authService.LoginAsync(model);
            if (!success)
                return Unauthorized(new { message });

            return Ok(new { token, message });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var success = await _authService.ConfirmEmailAsync(userId, token);
            if (!success)
                return BadRequest("Email confirmation failed.");

            return Ok("Email confirmed successfully!");
        }
    }
}
