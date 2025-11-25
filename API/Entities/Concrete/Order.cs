using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : BaseEntity
    {
        public int KullaniciId { get; set; }
        //public virtual Kullanici? Kullanici { get; set; }
        
        public int? PaymentId { get; set; }
        //public virtual Payment? Payment { get; set; }
        
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        
        // Sipariş Durumu: Pending, PaymentReceived, Preparing, Shipping, Delivered, Cancelled
        public string OrderStatus { get; set; } = "Pending";
        
        // Ödeme Durumu: Pending, Paid, Failed, Refunded
        public string PaymentStatus { get; set; } = "Pending";
        
        // Adres Bilgileri
        public string ShippingContactName { get; set; } = string.Empty;
        public string ShippingPhone { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;
        public string ShippingDistrict { get; set; } = string.Empty;
        public string ShippingZipCode { get; set; } = string.Empty;
        
        public string BillingContactName { get; set; } = string.Empty;
        public string BillingPhone { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string BillingCity { get; set; } = string.Empty;
        public string BillingDistrict { get; set; } = string.Empty;
        public string BillingZipCode { get; set; } = string.Empty;
        
        // Sipariş Kalemleri (JSON olarak saklanacak)
        public string OrderItems { get; set; } = string.Empty; // JSON: [{productId, name, price, quantity}]
        
        // Kurye/Kargo Bilgileri
        public string? CargoCompany { get; set; }
        public string? TrackingNumber { get; set; }
        
        // Notlar
        public string? CustomerNote { get; set; }
        public string? AdminNote { get; set; }
        
        // İptal bilgisi
        public string? CancellationReason { get; set; }
        public DateTime? CancelledAt { get; set; }
        
        // Teslim bilgisi
        public DateTime? DeliveredAt { get; set; }
    }
}
