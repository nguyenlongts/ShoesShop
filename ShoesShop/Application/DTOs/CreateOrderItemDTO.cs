namespace ShoesShop.Application.DTOs
{
    public class CreateOrderItemDto
    {
        public int ProductDetailId { get; set; } // ID của sản phẩm
        public int Quantity { get; set; }        // Số lượng sản phẩm
        public decimal PriceAtOrder { get; set; } // Giá sản phẩm tại thời điểm đặt hàng
    }

}
