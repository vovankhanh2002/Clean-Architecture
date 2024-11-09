using eComm.Application.DTOs.Category;
using eComm.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace eComm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await categoryService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await categoryService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Createcategory(CreateCategory category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryService.CreateAsync(category);
            return result.Flag == true ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Updatecategory(UpdateCategory category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryService.UpdateAsync(category);
            return result.Flag == true ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Deletecategory(int id)
        {
            var result = await categoryService.DeleteAsync(id);
            return result.Flag == true ? Ok(result) : BadRequest(result);
        }
    }
}
