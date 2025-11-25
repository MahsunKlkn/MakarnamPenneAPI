using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Review : BaseEntity
    {
        //public int SKUId { get; set; }
        //public virtual SKU SKU { get; set; }
        public int KullaniciId { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public int Rating { get; set; } // 1-5 arası
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
