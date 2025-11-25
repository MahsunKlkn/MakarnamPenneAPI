using System;
using System.Collections.Generic;
using Entities.Base;

namespace Entities.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }             // Kategori adı
        public string Description { get; set; }      // Kategori açıklaması
        public string ImageUrl { get; set; }         // Kategori görseli (ikon veya banner)

        public virtual ICollection<Product> Products { get; set; } // Bu kategoriye ait ürünler
    }
}
