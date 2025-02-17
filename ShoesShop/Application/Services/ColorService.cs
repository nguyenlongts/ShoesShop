using ShoesShop.Domain.Entities;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;

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
            var color = await _genericRepository.GetByIdAsync(id);
            if (color == null || !color.IsActive) return false;

            color.IsActive = false;
            await _genericRepository.UpdateAsync(color);
            return true;
        }

        public async Task<IEnumerable<Color>> GetAllColorsAsync()
        {
            return (await _genericRepository.GetAllAsync()).Where(c => c.IsActive);
        }

        public async Task<Color> GetColorByNameAsync(string name)
        {
            var color = await _colorRepository.GetByNameAsync(name);
            return color;
        }

        public async Task<bool> UpdateColorAsync(Color model)
        {
            await _genericRepository.UpdateAsync(model);
            return true;
        }
    }
}
