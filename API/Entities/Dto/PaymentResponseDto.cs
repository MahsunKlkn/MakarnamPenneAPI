using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class PaymentResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ThreeDSHtmlContent { get; set; }
        public string? PaymentId { get; set; }
        public string? ConversationId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
    
    public class PaymentCallbackDto
    {
        public string Status { get; set; } = string.Empty;
        public string PaymentId { get; set; } = string.Empty;
        public string ConversationId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
    
    public class PaymentResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal? PaidPrice { get; set; }
        public string? Currency { get; set; }
        public string? PaymentId { get; set; }
        public string? ConversationId { get; set; }
        public string? CardFamily { get; set; }
        public string? CardType { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
