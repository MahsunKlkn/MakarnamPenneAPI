using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _basketService.GetAllBasket();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var basket = _basketService.GetById(id);
            if (basket == null)
                return NotFound();

            return Ok(basket);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Basket basket)
        {
            _basketService.AddBasket(basket);
            return Ok("Sepet eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Basket basket)
        {
            var existing = _basketService.GetById(id);
            if (existing == null)
                return NotFound();

            basket.Id = id;
            _basketService.UpdateBasket(basket);
            return Ok("Sepet güncellendi.");
        }

        // Kullanıcı ID'si ile güncelleme: PUT api/basket/user/5
        // Gönderilen body içinde sadece ProductIds doldurulması yeterli.
        [HttpPut("user/{kullaniciId}")]
        public IActionResult UpdateByUserId(int kullaniciId, [FromBody] Basket basketUpdate)
        {
            var updated = _basketService.UpdateBasketByKullaniciId(kullaniciId, basketUpdate.ProductIds);
            if (updated == null)
                return NotFound("Kullanıcıya ait güncellenecek sepet bulunamadı.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var basket = _basketService.GetById(id);
            if (basket == null)
                return NotFound();

            _basketService.DeleteBasket(basket);
            return Ok("Sepet silindi.");
        }

        [HttpGet("GetByKullaniciId/{kullaniciId}")]
        public IActionResult GetByKullaniciId(int kullaniciId)
        {
            var result = _basketService.GetBasketsByKullaniciId(kullaniciId);
            if (result == null || !result.Any())
            {
                return NotFound("Kullanıcıya ait sepet bulunamadı.");
            }
            return Ok(result);
        }
    }
}