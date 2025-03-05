using API_ShoesShop.Infrastructure.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.DTOs;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProductDetailController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet("{productId}/details")]
        public async Task<IActionResult> GetProductDetails(int productId)
        {
            var productDetails = await _context.ProductDetails
                .Where(pd => pd.ProductId == productId)
                .Include(pd => pd.Color) // Load bảng Color
                .Include(pd => pd.Size)  // Load bảng Size
                .Select(pd => new
                {
                    pd.ProductDetailId,
                    pd.ProductId,
                    pd.ColorId,
                    ColorName = pd.Color != null ? pd.Color.Name : null,
                    pd.SizeId,
                    SizeName = pd.Size != null ? pd.Size.Name : null,
                    pd.Price,
                    pd.StockQuantity,
                    pd.ImageUrl
                })
                .ToListAsync();

            if (!productDetails.Any())
            {
                return NotFound("No product details found.");
            }

            return Ok(productDetails);
        }



        [HttpPost]
        public async Task<IActionResult> CreateProductDetail([FromBody] CreateProductDetailDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Kiểm tra sản phẩm có tồn tại không
                var product = await _context.Products.FindAsync(model.ProductId);
                if (product == null)
                    return NotFound("Sản phẩm không tồn tại.");

                // Kiểm tra biến thể có tồn tại chưa
                bool exists = _context.ProductDetails.Any(pd =>
                    pd.ProductId == model.ProductId &&
                    pd.ColorId == model.ColorID &&
                    pd.SizeId == model.SizeID);


                if (exists)
                    return BadRequest($"Biến thể đã tồn tại.");

                // Thêm biến thể mới
                var productDetail = new ProductDetail
                {
                    ProductId = model.ProductId,
                    ColorId = model.ColorID,
                    SizeId = model.SizeID,
                    StockQuantity = model.Quantity,
                    Price = model.Price,
                    ImageUrl = model.ImageUrls != null ? string.Join(";", model.ImageUrls) : null,
                };


                _context.ProductDetails.Add(productDetail);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Thêm biến thể thành công!", productDetail });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
    }
}