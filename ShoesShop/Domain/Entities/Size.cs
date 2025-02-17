using System.ComponentModel.DataAnnotations;

namespace ShoesShop.Domain.Entities
{
    public class Size
    {
        [Key]
        public int SizeId { get; set; }

        public int Name { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
