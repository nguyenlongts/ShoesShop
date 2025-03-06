using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Application.Services;

namespace ShoesShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart ([FromBody] AddToCartRequest request) { 
            var result = await _cartService.AddItemAsync(request);
            if (result == true)
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }
    }
}
