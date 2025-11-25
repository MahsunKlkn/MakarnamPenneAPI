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
    public class EfCategoryRepository : GenericRepository<Category>, ICategoryDal
    {
        public EfCategoryRepository(Context.Context context) : base(context)
        {
        }
    }
}
