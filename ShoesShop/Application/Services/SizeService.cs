using ShoesShop.Domain.Entities;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.DTOs;

namespace ShoesShop.Application.Services
{
    public class SizeService : ISizeService
    {

        private readonly IGenericRepository<Size> _genericRepository;
        private readonly ISizeRepository _SizeRepository;
        public SizeService(ISizeRepository SizeRepository, IGenericRepository<Size> genericRepository)
        {
            _SizeRepository = SizeRepository;
            _genericRepository = genericRepository;
        }
        public async Task<bool> CreateSizeAsync(Size model)
        {
            model.IsActive = true;
            await _SizeRepository.AddAsync(model);
            return true;
        }

        public async Task<bool> DeleteSizeAsync(int id)
        {
 
            await _genericRepository.DeleteAsync(id);
            return true;
        }

        public async Task<ResponseDTO<Size>> GetAllSizeAsync(int pageNumber, int pageSize)
        {
            return await _genericRepository.GetAllAsync(pageNumber,pageSize);
        }

        public async Task<Size> GetSizeByNameAsync(string name)
        {
            var Size = await _SizeRepository.GetByNameAsync(name);
            return Size;
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
        

        public async Task<bool> UpdateSizeAsync(Size model)
        {
            await _genericRepository.UpdateAsync(model);
            return true;
        }
    }
}
