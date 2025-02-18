using ShoesShop.Domain.Entities;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ShoesShop.Application.Services
{
    public class ColorService : IColorService
    {

        private readonly IGenericRepository<Color> _genericRepository;
        private readonly IColorRepository _colorRepository;
        public ColorService(IColorRepository colorRepository, IGenericRepository<Color> genericRepository)
        {
            _colorRepository = colorRepository;
            _genericRepository = genericRepository;
        }
        public async Task<bool> CreateColorAsync(Color model)
        {
            model.IsActive = true; // Khi tạo mới, luôn active
            await _genericRepository.AddAsync(model);
            return true;
        }

        public async Task<bool> DeleteColorAsync(int id)
        {
 
            await _genericRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<Color>> GetAllColorsAsync(int pageNumber, int pageSize)
        {
            return await _genericRepository.GetAllAsync(pageNumber,pageSize);
        }

        public async Task<Color> GetColorByNameAsync(string name)
        {
            var color = await _colorRepository.GetByNameAsync(name);
            return color;
        }

        public async Task<bool> UpdateStatusAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            // Cập nhật trạng thái
            entity.IsActive = !entity.IsActive;
            await _genericRepository.UpdateAsync(entity);

            // Trả về true nếu update thành công
            return true;
        }
        

        public async Task<bool> UpdateColorAsync(Color model)
        {
            await _genericRepository.UpdateAsync(model);
            return true;
        }
    }
}
