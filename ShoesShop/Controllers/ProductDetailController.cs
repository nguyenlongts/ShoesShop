using API_ShoesShop.Infrastructure.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Application.Services;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IFileService _fileService;
        public ProductDetailController(AppDBContext context,  IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
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
        public async Task<IActionResult> CreateProductDetail([FromForm] CreateProductDetailDTO model)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _context.Products.FindAsync(model.ProductId);
            if (product == null)
                return NotFound("Sản phẩm không tồn tại.");

            bool exists = _context.ProductDetails.Any(pd =>
                pd.ProductId == model.ProductId &&
                pd.ColorId == model.ColorID &&
                pd.SizeId == model.SizeID);


            if (exists)
                return BadRequest($"Biến thể đã tồn tại.");
            //List<string> fileUrls = new List<string>();

            //if (model.Images != null && model.Images.Count > 0)
            //{
            //    if (model.Images.Count > 3)
            //        return BadRequest("Bạn chỉ được tải lên tối đa 3 ảnh!");

            //    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

            //    if (!Directory.Exists(uploadsFolder))
            //        Directory.CreateDirectory(uploadsFolder);

            //    foreach (var image in model.Images)
            //    {
            //        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            //        var filePath = Path.Combine(uploadsFolder, fileName);

            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await image.CopyToAsync(stream);
            //        }

            //        fileUrls.Add($"/uploads/{fileName}");
            //    }
            //}
            if (model.Images?.Length > 1 * 2048 * 2048)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
            }
            string[] allowedFileExtentions = [".jpg", ".jpeg", ".png",".PNG"];
            string createdImageName = await _fileService.SaveFileAsync(model.Images, allowedFileExtentions);

            var productDetail = new ProductDetail
            {
                ProductId = model.ProductId,
                ColorId = model.ColorID,
                SizeId = model.SizeID,
                StockQuantity = model.Quantity,
                Price = model.Price,
                ImageUrl = createdImageName
            };


            _context.ProductDetails.Add(productDetail);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Thêm biến thể thành công!", productDetail });


        }
    }
}