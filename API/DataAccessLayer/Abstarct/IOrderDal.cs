using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstarct
{
    public interface IOrderDal : IGenericDal<Order>
    {
        List<Order> GetByKullaniciId(int kullaniciId);
        List<Order> GetByOrderStatus(string status);
        List<Order> GetByPaymentStatus(string status);
    }
}
