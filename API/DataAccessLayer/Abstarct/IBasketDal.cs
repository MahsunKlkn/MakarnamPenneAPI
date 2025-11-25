using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstarct
{
    public interface IBasketDal : IGenericDal<Basket>
    {
        List<Basket> GetBasketsByKullaniciId(int kullaniciId);
    }
}
