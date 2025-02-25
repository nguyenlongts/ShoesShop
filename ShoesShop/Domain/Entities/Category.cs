using System.ComponentModel.DataAnnotations;

namespace ShoesShop.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CateID { get; set; }
        public string? Name { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
