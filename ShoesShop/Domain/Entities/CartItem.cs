using System.ComponentModel.DataAnnotations;

namespace ShoesShop.Domain.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public int ProductDetailId {  get; set; }
        public ProductDetail ProductDetail { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Quantity * (ProductDetail.Price);
    }
}
