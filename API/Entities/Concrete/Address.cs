using Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Address : BaseEntity
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? AddressTitle { get; set; } // Örn: "Ev Adresim", "İş Adresim"

        // Foreign Key
        public string? KullaniciId { get; set; }

        [NotMapped]
        public virtual Kullanici Kullanici { get; set; }
    }
}
