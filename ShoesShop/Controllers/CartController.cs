using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Application.Services;
using ShoesShop.Domain.Entities;

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
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart ([FromBody] AddToCartRequest request) { 
            var result = await _cartService.AddItemAsync(request);
            if (result == true)
            {
                return Ok("Thêm thành công");
            }
            return Ok("Thêm thất bại");
        }

        [HttpGet("GetCartById/{id}")]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var cart =await _cartService.GetByUserIdAsync(id);
            if (cart == null)
            {
                await   _cartService.CreateAsync(id);
            }
            return Ok(cart);
        }
        [HttpGet("GetAllCartItems")]
        public async Task<IActionResult> GetAllCartItems(Guid userId)
        {
            return Ok(await _cartService.GetAllCartItem(userId));
        }
        [HttpPost("UpdateQuantity/{cartItemId}")]
        public async Task<IActionResult> UpdateQuantity ([FromBody]UpdateCIRequest request)
        {
            return Ok(await _cartService.UpdateQuantityAsync(request.UserId, request.CartItemId, request.Quantity));
        }
    }
}
