using eComm.Application.DTOs.Product;
using eComm.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace eComm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await productService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await productService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(CreateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); 
            var result = await productService.CreateAsync(product);
            return result.Flag == true ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct(UpdateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await productService.UpdateAsync(product);
            return result.Flag == true ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await productService.DeleteAsync(id);
            return result.Flag == true ? Ok(result) : BadRequest(result);
        }
    }
}
