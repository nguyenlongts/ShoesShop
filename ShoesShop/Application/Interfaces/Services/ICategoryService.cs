using System.Threading.Tasks;
using ShoesShop.Application.DTOs;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync(int pageSize,int pageNum);

        Task<bool> CreateCategoryAsync(CreateCateDTO model);
        Task<Category> GetCategoryByNameAsync(string name);
        Task<bool> UpdateStatusAsync(int CategoryID);
    }
}
