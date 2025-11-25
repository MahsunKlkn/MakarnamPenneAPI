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
    public class EfBrandRepository : GenericRepository<Brand>, IBrandDal
    {
        public EfBrandRepository(Context.Context context) : base(context)
        {
        }
    }
}
