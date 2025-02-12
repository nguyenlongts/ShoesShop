namespace API_ShoesShop.Application.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
