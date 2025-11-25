using System;
using Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }            
        public string? Description { get; set; }     
        public decimal Price { get; set; }           
        public double? DiscountRate { get; set; }    
        public int StockQuantity { get; set; }       
        public string? SKU { get; set; }             
        public bool IsActive { get; set; } = true;  
        public string? ImageUrl { get; set; }        
        public int CategoryId { get; set; }
    }
}
