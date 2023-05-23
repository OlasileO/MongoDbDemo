using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDbDemo.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDbDemo.Controllers
{
    
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _productService.GetAllAsync();
            return Ok(category);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _productService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Product product)
        {
            product.CategoryName = null;
            await _productService.CreateAsync(product);
            return Ok("Create successfully!");
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] Product newproduct)
        {
            newproduct.CategoryName = null;
            var category = await _productService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            await _productService.UpdateAsync(id, newproduct);
            return Ok("Update Successfully!");
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _productService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            await _productService.DeleteAsync(id);
            return Ok("Delete Successfully!");
        }
    }
}
