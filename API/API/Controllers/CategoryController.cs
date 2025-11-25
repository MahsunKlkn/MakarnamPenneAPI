using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _categoryService.GetAllCategory(null);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Category category)
        {
            _categoryService.AddCategory(category);
            return Ok("Kategori eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            var existing = _categoryService.GetById(id);
            if (existing == null)
                return NotFound();

            category.Id = id;
            _categoryService.UpdateCategory(category);
            return Ok("Kategori güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound();

            _categoryService.DeleteCategory(category);
            return Ok("Kategori silindi.");
        }
    }
}