using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetAllAsync());
        }

        [HttpPost("Create")]

        public async Task<IActionResult> Create(Brand brand)
        {
            var result = await _brandService.CreateBrandAsync(brand);
            if (result)
            {
                return Ok("Create brand " + brand.Name +" successfully" );
            }
            return BadRequest("Create failed");

        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
        {
            if (request == null || (request.newStatus != 0 && request.newStatus != 1))
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var result = await _brandService.UpdateStatusAsync(request.BrandID, request.newStatus);
            if (!result)
            {
                return NotFound(new { message = "Brand không tồn tại hoặc cập nhật thất bại" });
            }

            return Ok(new { message = "Cập nhật trạng thái thành công" });
        }
    }
}
