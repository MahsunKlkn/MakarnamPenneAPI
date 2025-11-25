using Entities.Base;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class DtoKullaniciToken
    {
        public Kullanici? Kullanici { get; set; }
        public string? KullaniciId { get; set; }
    }
}