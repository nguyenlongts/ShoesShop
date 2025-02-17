namespace ShoesShop.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } // Giày Nike vải
        public string Description { get; set; }
        public decimal BasePrice { get; set; } // Giá cơ bản (nếu có)
        public string Image { get; set; } // Ảnh đại diện chung
        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
