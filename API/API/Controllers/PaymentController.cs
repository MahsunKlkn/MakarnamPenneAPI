using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Ödeme işlemini başlatır ve 3D Secure HTML içeriğini döner
        /// </summary>
        [HttpPost("initiate")]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentRequestDto request)
        {
            try
            {
                var result = await _paymentService.InitiatePayment(request);
                
                if (!result.Success)
                {
                    return BadRequest(new 
                    { 
                        success = false, 
                        message = result.Message,
                        errorCode = result.ErrorCode,
                        errorMessage = result.ErrorMessage
                    });
                }

                return Ok(new 
                { 
                    success = true,
                    message = result.Message,
                    threeDSHtmlContent = result.ThreeDSHtmlContent,
                    paymentId = result.PaymentId,
                    conversationId = result.ConversationId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                { 
                    success = false, 
                    message = "Ödeme işlemi başlatılamadı", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// İyzico'dan dönen callback'i işler
        /// </summary>
        [HttpPost("callback")]
        public async Task<IActionResult> PaymentCallback([FromForm] PaymentCallbackDto callback)
        {
            try
            {
                var result = await _paymentService.HandleCallback(callback);
                
                if (!result.Success)
                {
                    return BadRequest(new 
                    { 
                        success = false, 
                        message = result.Message,
                        status = result.Status
                    });
                }

                return Ok(new 
                { 
                    success = true,
                    message = result.Message,
                    status = result.Status,
                    paidPrice = result.PaidPrice,
                    currency = result.Currency,
                    paymentId = result.PaymentId,
                    conversationId = result.ConversationId,
                    cardFamily = result.CardFamily,
                    cardType = result.CardType,
                    paymentDate = result.PaymentDate
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                { 
                    success = false, 
                    message = "Ödeme callback işlenemedi", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Conversation ID ile ödeme bilgisini getirir
        /// </summary>
        [HttpGet("conversation/{conversationId}")]
        public IActionResult GetByConversationId(string conversationId)
        {
            var payment = _paymentService.GetByConversationId(conversationId);
            
            if (payment == null)
                return NotFound(new { success = false, message = "Ödeme bulunamadı" });

            return Ok(new 
            { 
                success = true,
                data = payment
            });
        }

        /// <summary>
        /// Payment ID ile ödeme bilgisini getirir
        /// </summary>
        [HttpGet("payment/{paymentId}")]
        public IActionResult GetByPaymentId(string paymentId)
        {
            var payment = _paymentService.GetByPaymentId(paymentId);
            
            if (payment == null)
                return NotFound(new { success = false, message = "Ödeme bulunamadı" });

            return Ok(new 
            { 
                success = true,
                data = payment
            });
        }

        /// <summary>
        /// Order ID'ye göre tüm ödemeleri getirir
        /// </summary>
        [HttpGet("order/{orderId}")]
        public IActionResult GetByOrderId(int orderId)
        {
            var payments = _paymentService.GetByOrderId(orderId);
            
            if (payments == null || !payments.Any())
                return NotFound(new { success = false, message = "Sipariş için ödeme bulunamadı" });

            return Ok(new 
            { 
                success = true,
                data = payments
            });
        }
    }
}
