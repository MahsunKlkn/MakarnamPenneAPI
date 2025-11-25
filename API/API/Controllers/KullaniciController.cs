using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciService _kullaniciService;

        public KullaniciController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _kullaniciService.GetAllKullanici();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var kullanici = _kullaniciService.GetById(id);
            if (kullanici == null)
                return NotFound();

            return Ok(kullanici);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Kullanici kullanici)
        {
            _kullaniciService.AddKullanici(kullanici);
            return Ok("Kullanıcı eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Kullanici kullanici)
        {
            var existing = _kullaniciService.GetById(id);
            if (existing == null)
                return NotFound();

            kullanici.Id = id;
            _kullaniciService.UpdateKullanici(kullanici);
            return Ok("Kullanıcı güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var kullanici = _kullaniciService.GetById(id);
            if (kullanici == null)
                return NotFound();

            _kullaniciService.DeleteKullanici(kullanici);
            return Ok("Kullanıcı silindi.");
        }
    }
}
