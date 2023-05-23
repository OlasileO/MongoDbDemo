using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDbDemo.Service;

namespace MongoDbDemo.Controllers
{
    
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _categoryService.GetAllAsync();
            return Ok(category);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
           var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult>  CreateCategory([FromBody] Category category)
        {
            await _categoryService.CreateAsync(category);
            return Ok("Create successfully!");
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] Category updatecategory)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if(category == null)
                return NotFound();
            await _categoryService.UpdateAsync(id, updatecategory);
            return Ok("Update Successfully!");
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            await _categoryService.DeleteAsync(id);
            return Ok("Delete Successfully!");
        }
    }
}
