using Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Basket : BaseEntity
    {
        public int KullaniciId { get; set; }
        public string? ProductIds { get; set; }

       //[NotMapped]
       //public virtual Kullanici Kullanici { get; set; }
        
    }
}
