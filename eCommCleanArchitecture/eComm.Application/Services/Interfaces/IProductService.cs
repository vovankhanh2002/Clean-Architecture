using eComm.Application.DTOs;
using eComm.Application.DTOs.Product;

namespace eComm.Application.Services.Interfaces
{
    public interface IProductService 
    {
        Task<IEnumerable<GetProduct>> GetAllAsync();
        Task<GetProduct> GetByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(CreateProduct entity);
        Task<ServiceResponse> UpdateAsync(UpdateProduct entity);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}
