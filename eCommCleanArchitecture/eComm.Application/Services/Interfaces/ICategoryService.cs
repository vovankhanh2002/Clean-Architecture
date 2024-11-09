using eComm.Application.DTOs;
using eComm.Application.DTOs.Category;

namespace eComm.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(CreateCategory entity);
        Task<ServiceResponse> UpdateAsync(UpdateCategory entity);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}
