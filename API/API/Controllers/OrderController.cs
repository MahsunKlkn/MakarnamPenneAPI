using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Tüm siparişleri getirir (Admin)
        /// </summary>
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(new { success = true, data = orders });
        }

        /// <summary>
        /// ID'ye göre sipariş getirir
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var order = _orderService.GetById(id);
                if (order == null)
                    return NotFound(new { success = false, message = "Sipariş bulunamadı" });

                return Ok(new { success = true, data = order });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Kullanıcının siparişlerini getirir
        /// </summary>
        [HttpGet("user/{kullaniciId}")]
        public IActionResult GetByKullaniciId(int kullaniciId)
        {
            var orders = _orderService.GetByKullaniciId(kullaniciId);
            return Ok(new { success = true, data = orders });
        }

        /// <summary>
        /// Duruma göre siparişleri getirir
        /// </summary>
        [HttpGet("status/{status}")]
        public IActionResult GetByStatus(string status)
        {
            var orders = _orderService.GetByOrderStatus(status);
            return Ok(new { success = true, data = orders });
        }

        /// <summary>
        /// Yeni sipariş oluşturur
        /// </summary>
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            try
            {
                var order = _orderService.CreateOrder(orderDto);
                return Ok(new 
                { 
                    success = true, 
                    message = "Sipariş oluşturuldu", 
                    data = order 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Sipariş durumunu günceller (Admin)
        /// </summary>
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] OrderUpdateStatusDto statusDto)
        {
            try
            {
                var order = _orderService.UpdateOrderStatus(id, statusDto);
                return Ok(new 
                { 
                    success = true, 
                    message = "Sipariş durumu güncellendi", 
                    data = order 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Siparişi iptal eder
        /// </summary>
        [HttpPut("{id}/cancel")]
        public IActionResult CancelOrder(int id, [FromBody] OrderCancelDto cancelDto)
        {
            try
            {
                var order = _orderService.CancelOrder(id, cancelDto);
                return Ok(new 
                { 
                    success = true, 
                    message = "Sipariş iptal edildi", 
                    data = order 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Siparişi teslim edildi olarak işaretle
        /// </summary>
        [HttpPut("{id}/deliver")]
        public IActionResult MarkAsDelivered(int id)
        {
            try
            {
                var order = _orderService.MarkAsDelivered(id);
                return Ok(new 
                { 
                    success = true, 
                    message = "Sipariş teslim edildi olarak işaretlendi", 
                    data = order 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Siparişi siler (Admin)
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return Ok(new { success = true, message = "Sipariş silindi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
