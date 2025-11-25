using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> InitiatePayment(PaymentRequestDto request);
        Task<PaymentResultDto> HandleCallback(PaymentCallbackDto callback);
        Payment? GetByConversationId(string conversationId);
        Payment? GetByPaymentId(string paymentId);
        List<Payment> GetByOrderId(int orderId);
    }
}
