namespace ShoesShop.Domain.Entities
{
    public class Brand
    {
        public Guid BrandID { get; set; }
        public string? Name { get; set; }

        public bool IsActive { get; set; }
    }
}
