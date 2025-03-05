using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync(int pageNumber=1,int pageSize=5)
        {
            var result = await _colorService.GetAllColorsAsync(pageNumber, pageSize);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Color model)
        {
            var result = await _colorService.CreateColorAsync(model);
            if (result == false) {
                return BadRequest("Add failed");
            }
            return Ok("Add color successfully");
        }
        [Authorize]
        [HttpGet("GetByName")]        
        public async Task<Color> GetColorByNameAsync(string name)
        {
            return await _colorService.GetColorByNameAsync(name);
        }
        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Color model)
        {
            var result =await _colorService.UpdateColorAsync(model);
            if (result == true)
            {
                return Ok("Update color successfully");
            }
            return BadRequest("Update failed");
        }
        [Authorize]
        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var result = await _colorService.UpdateStatusAsync(id);

            if (!result)
            {
                return NotFound("Color not found");
            }

            // Nếu update thành công
            return Ok("Color status updated successfully");
        }
        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _colorService.DeleteColorAsync(id);
            if(result == true)
            {
                return Ok("Delete Successfully");
            }
            return BadRequest("Delete Failed");
        }
    }
    
}
