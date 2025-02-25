namespace ShoesShop.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public decimal BasePrice { get; set; } 
        public string Image { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CateId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
