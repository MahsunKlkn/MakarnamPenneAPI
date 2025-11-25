using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _productService.GetAllProduct();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            _productService.AddProduct(product);
            return Ok("Ürün eklendi.");
        }

        // ProductController.cs - DOĞRU KOD
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            product.Id = id;
            _productService.UpdateProduct(product);
            return Ok("Ürün güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();

            _productService.DeleteProduct(product);
            return Ok("Ürün silindi.");
        }
    }
}