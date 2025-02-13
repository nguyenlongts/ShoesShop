namespace ShoesShop.Application.DTOs
{
    public class UpdateStatusRequest
    {
        public Guid BrandID { get; set; }

        public int newStatus { get; set; }
    }
}
