using API_ShoesShop.Domain.Entities;

namespace ShoesShop.Domain.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }= Guid.NewGuid();
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal TotalPrice => CartItems.Sum(item => item.Total);
    }
}
