using System.IdentityModel.Tokens.Jwt;
using API_ShoesShop.Application.DTOs;
using API_ShoesShop.Application.Services;
using API_ShoesShop.Domain.Entities;
using API_ShoesShop.Infrastructure.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_ShoesShop.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;
        public AuthController(UserManager<ApplicationUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        [HttpOptions("register")]
        public IActionResult Preflight()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                PasswordHash = model.Password,
                Address = model.Address,
                Email = model.Email,
                DoB = model.DoB,
                PhoneNumber = model.Phone,
                Gender = model.Gender,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Registration successful!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return Unauthorized("Invalid password");
            }
            var token = await _tokenService.GenerateToken(user);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expireTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp).Value)).UtcDateTime;
            var response = new LoginResponse
            {
                ExpireTime = expireTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Token = token
            };
            return Ok(response);
        }
    }
}
