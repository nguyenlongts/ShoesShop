using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Services
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetAllColorsAsync(); // Lấy danh sách tất cả Color
        Task<Color> GetColorByNameAsync(string name); // Lấy Color theo ID
        Task<bool> CreateColorAsync(Color model);
        Task<bool> UpdateColorAsync(Color model);
        Task<bool> DeleteColorAsync(int id); // Xóa Color theo ID
    }

}
