using System;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class OrderCreateDto
    {
        public int KullaniciId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        
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
        
        // Sipariş Kalemleri
        public List<OrderItemDto> OrderItems { get; set; } = new();
        
        public string? CustomerNote { get; set; }
    }
    
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    
    public class OrderUpdateStatusDto
    {
        public string OrderStatus { get; set; } = string.Empty;
        public string? AdminNote { get; set; }
        public string? CargoCompany { get; set; }
        public string? TrackingNumber { get; set; }
    }
    
    public class OrderCancelDto
    {
        public string CancellationReason { get; set; } = string.Empty;
    }
}
