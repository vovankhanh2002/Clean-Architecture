using AutoMapper;
using eComm.Application.DTOs;
using eComm.Application.DTOs.Category;
using eComm.Application.Services.Interfaces;
using eComm.Domain.Entities;
using eComm.Domain.Interfaces;
namespace eComm.Application.Services.Implementations
{
    public class CategoryService(IGeneric<Category> CategoryGeneric, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> CreateAsync(CreateCategory entity)
        {
            var mapperData = mapper.Map<Category>(entity);
            int result = await CategoryGeneric.CreateAsync(mapperData);
            return result > 0 ? new ServiceResponse(true, "Category create!") : new ServiceResponse(false, "Category fail create!");

        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            int result = await CategoryGeneric.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Category delete!") : new ServiceResponse(false, "Category fail delete!");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rawData = await CategoryGeneric.GetAllAsync();
            if (!rawData.Any()) return [];
            return mapper.Map<IEnumerable<GetCategory>>(rawData);
        }

        public async Task<GetCategory> GetByIdAsync(int id)
        {
            var rawData = await CategoryGeneric.GetByIdAsync(id);
            if (rawData == null) return new GetCategory();
            return mapper.Map<GetCategory>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory entity)
        {
            var mapperData = mapper.Map<Category>(entity);
            int result = await CategoryGeneric.UpdateAsync(mapperData);
            return result > 0 ? new ServiceResponse(true, "Category update!") : new ServiceResponse(false, "Category fail update!");
        }
    }
}
