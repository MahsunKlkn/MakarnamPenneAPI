using Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Kullanici : BaseEntity
    {
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Eposta { get; set; }
        public string? Sifre { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public Int32 Rol { get; set; }
        public string? Telefon { get; set; }

        public string? AddressId { get; set; }
        [NotMapped]
        public virtual Address? Address { get; set; }

    }
}