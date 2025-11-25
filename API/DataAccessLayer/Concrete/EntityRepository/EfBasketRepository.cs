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
    public class EfBasketRepository : GenericRepository<Basket>, IBasketDal
    {
        public EfBasketRepository(Context.Context context) : base(context)
        {
        }

        public List<Basket> GetBasketsByKullaniciId(int kullaniciId)
        {
            return _context.Basket.Where(b => b.KullaniciId == kullaniciId).ToList();
        }
    }
}
