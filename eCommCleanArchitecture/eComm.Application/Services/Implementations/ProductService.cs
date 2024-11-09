using AutoMapper;
using eComm.Application.DTOs;
using eComm.Application.DTOs.Product;
using eComm.Application.Services.Interfaces;
using eComm.Domain.Entities;
using eComm.Domain.Interfaces;

namespace eComm.Application.Services.Implementations
{
    public class ProductService(IGeneric<Product> ProductGeneric, IMapper mapper ) : IProductService
    {
        public async Task<ServiceResponse> CreateAsync(CreateProduct entity)
        {
            var mapperData = mapper.Map<Product>(entity);
            int result = await ProductGeneric.CreateAsync(mapperData);
            return result > 0 ? new ServiceResponse(true, "Product create!") : new ServiceResponse(false, "Product fail create!");

        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            int result = await ProductGeneric.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Product delete!") : new ServiceResponse(false, "Product fail delete!");
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var rawData = await ProductGeneric.GetAllAsync();
            if (!rawData.Any()) return [];
            return mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<GetProduct> GetByIdAsync(int id)
        {
            var rawData = await ProductGeneric.GetByIdAsync(id);
            if(rawData == null) return new GetProduct();
            return mapper.Map<GetProduct>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct entity)
        {
            var mapperData = mapper.Map<Product>(entity);
            int result = await ProductGeneric.UpdateAsync(mapperData);
            return result > 0 ? new ServiceResponse(true, "Product update!") : new ServiceResponse(false, "Product fail update!");
        }
    }
}
