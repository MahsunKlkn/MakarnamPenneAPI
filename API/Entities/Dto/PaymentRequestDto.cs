using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class PaymentRequestDto
    {
        public decimal Price { get; set; }
        public decimal PaidPrice { get; set; }
        public string Currency { get; set; } = "TRY";
        public int BasketId { get; set; }
        public string CallbackUrl { get; set; } = string.Empty;
        
        // Buyer Information
        public string BuyerId { get; set; } = string.Empty;
        public string BuyerName { get; set; } = string.Empty;
        public string BuyerSurname { get; set; } = string.Empty;
        public string BuyerEmail { get; set; } = string.Empty;
        public string BuyerIdentityNumber { get; set; } = string.Empty;
        public string BuyerRegistrationAddress { get; set; } = string.Empty;
        public string BuyerCity { get; set; } = string.Empty;
        public string BuyerCountry { get; set; } = string.Empty;
        public string BuyerZipCode { get; set; } = string.Empty;
        public string BuyerPhone { get; set; } = string.Empty;
        
        // Shipping Address
        public string ShippingContactName { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;
        public string ShippingCountry { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingZipCode { get; set; } = string.Empty;
        
        // Billing Address
        public string BillingContactName { get; set; } = string.Empty;
        public string BillingCity { get; set; } = string.Empty;
        public string BillingCountry { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string BillingZipCode { get; set; } = string.Empty;
        
        // Basket Items
        public List<PaymentBasketItemDto> BasketItems { get; set; } = new();
    }
    
    public class PaymentBasketItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category1 { get; set; } = string.Empty;
        public string ItemType { get; set; } = "PHYSICAL";
        public decimal Price { get; set; }
    }
}
