using DataAccessLayer.Abstarct;
using DataAccessLayer.Repositories;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityRepository
{
    public class EfOrderRepository : GenericRepository<Order>, IOrderDal
    {
        public EfOrderRepository(Context.Context context) : base(context)
        {
        }

        public List<Order> GetByKullaniciId(int kullaniciId)
        {
            return _context.Order.Where(o => o.KullaniciId == kullaniciId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> GetByOrderStatus(string status)
        {
            return _context.Order.Where(o => o.OrderStatus == status)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> GetByPaymentStatus(string status)
        {
            return _context.Order.Where(o => o.PaymentStatus == status)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }
    }
}
