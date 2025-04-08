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
    //public class ProductDetailController : ControllerBase
    //{
    //    private readonly AppDBContext _context;
    //    private readonly IFileService _fileService;
    //    public ProductDetailController(AppDBContext context, IFileService fileService)
    //    {
    //        _context = context;
    //        _fileService = fileService;
    //    }
    //    [HttpGet("{productId}/details")]
    //    public async Task<IActionResult> GetProductDetails(int productId)
    //    {
    //        var productDetails = await _context.ProductDetails
    //            .Where(pd => pd.ProductId == productId)
    //            .Include(pd => pd.Color)
    //            .Include(pd => pd.Size)
    //            .Select(pd => new
    //            {
    //                pd.ProductDetailId,
    //                pd.ProductId,
    //                pd.ColorId,
    //                ColorName = pd.Color != null ? pd.Color.Name : null,
    //                pd.SizeId,
    //                SizeName = pd.Size != null ? pd.Size.Name : null,
    //                pd.Price,
    //                pd.StockQuantity,
    //                pd.ImageUrl
    //            })
    //            .ToListAsync();

    //        if (!productDetails.Any())
    //        {
    //            return NotFound("No product details found.");
    //        }

    //        return Ok(productDetails);
    //    }

    //    [HttpGet("{id}")]
    //    public async Task<IActionResult> GetVariantById(int id)
    //    {
    //        return Ok(await _context.ProductDetails.FindAsync(id));
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> CreateProductDetail([FromForm] CreateProductDetailDTO model)
    //    {

    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);

    //        var product = await _context.Products.FindAsync(model.ProductId);
    //        if (product == null)
    //            return NotFound("Sản phẩm không tồn tại.");

    //        bool exists = _context.ProductDetails.Any(pd =>
    //            pd.ProductId == model.ProductId &&
    //            pd.ColorId == model.ColorID &&
    //            pd.SizeId == model.SizeID);


    //        if (exists)
    //            return BadRequest($"Biến thể đã tồn tại.");
    //        if (model.Image?.Length > 1 * 2048 * 2048)
    //        {
    //            return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
    //        }
    //        string[] allowedFileExtentions = [".jpg", ".jpeg", ".png", ".PNG"];
    //        string createdImageName = await _fileService.SaveFileAsync(model.Image, allowedFileExtentions);

    //        var productDetail = new ProductDetail
    //        {
    //            ProductId = model.ProductId,
    //            ColorId = model.ColorID,
    //            SizeId = model.SizeID,
    //            StockQuantity = model.Quantity,
    //            Price = model.Price,
    //            ImageUrl = createdImageName
    //        };
    //        _context.ProductDetails.Add(productDetail);
    //        await _context.SaveChangesAsync();
    //        return Ok(new { message = "Thêm biến thể thành công!", productDetail });
    //    }
    //}[Route("api/[controller]")]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [HttpGet("{productId}/details")]
        public async Task<IActionResult> GetProductDetails(int productId)
        {
            var result = await _productDetailService.GetByProductIdAsync(productId);
            if (!result.Any()) return NotFound("Không tìm thấy biến thể.");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVariantById(int id)
        {
            var productDetail = await _productDetailService.GetByIdAsync(id);
            return productDetail != null ? Ok(productDetail) : NotFound("Không tìm thấy biến thể.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail([FromForm] CreateProductDetailDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, message, productDetail) = await _productDetailService.CreateProductDetailAsync(model);

            if (!success)
                return BadRequest(new { message });

            return Ok(new { message, productDetail });
        }
    }

}