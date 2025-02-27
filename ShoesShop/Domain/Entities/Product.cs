using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShoesShop.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public float BasePrice { get; set; } 
        public string? Image { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        [JsonIgnore]
        public Brand Brand { get; set; }

        public int CateId { get; set; }
        [ForeignKey("CateId")]
        [JsonIgnore]
        public Category Category { get; set; }
        public ICollection<ProductDetail> ProductDetails { get; set; }

        public bool IsActive {  get; set; }
    }
}
