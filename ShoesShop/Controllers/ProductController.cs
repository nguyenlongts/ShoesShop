using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Services;

namespace ShoesShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet("GetAllAdmin")]
        public async Task<IActionResult> GetAllAdmin(int pageSize=5, int pageNum=1) {
            var products = await _productService.GetAllAdminAsync(pageSize, pageNum);
            if (products.Count() > 0)
            {
                return Ok(products);
            }
            return BadRequest("Get Failed");
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductDTO model) {
            var result = await _productService.CreateAsync(model);
            if (result)
            {
                return(Ok("Create successfully"));
            }
            return BadRequest("Create failed");
        }
    }
}
