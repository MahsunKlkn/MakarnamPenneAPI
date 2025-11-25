using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Payment : BaseEntity
    {
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }
        
        public string? PaymentId { get; set; } // Iyzico Payment ID
        public string ConversationId { get; set; } = string.Empty; // Benzersiz işlem takip ID'si
        public decimal Amount { get; set; }
        public decimal PaidPrice { get; set; }
        public string Currency { get; set; } = "TRY"; // TRY, USD, EUR
        
        public string Status { get; set; } = "Pending"; // Pending, Success, Failed, Cancelled
        public string? PaymentStatus { get; set; } // Iyzico'dan dönen status
        
        public string? CardFamily { get; set; }
        public string? CardType { get; set; }
        public string? CardAssociation { get; set; }
        public string? CardToken { get; set; }
        public string? LastFourDigits { get; set; }
        
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorGroup { get; set; }
        
        public string? ThreeDSHtmlContent { get; set; } // 3D Secure HTML içeriği
        public DateTime? PaymentDate { get; set; }
    }
}
