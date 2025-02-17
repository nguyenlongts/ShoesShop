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
        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            return await _colorService.GetAllColorsAsync();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Color model)
        {
            var result = await _colorService.CreateColorAsync(model);
            if (result == false) {
                return BadRequest("Add failed");
            }
            return Ok("Add color successfully");
        }
        [HttpGet("GetByName")]        
        
        public async Task<Color> GetColorByNameAsync(string name)
        {
            return await _colorService.GetColorByNameAsync(name);
        }
    }
}
