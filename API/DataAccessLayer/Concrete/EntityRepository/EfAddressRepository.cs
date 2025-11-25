using DataAccessLayer.Abstarct;
using DataAccessLayer.Repositories;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityRepository
{
    public class EfAddressRepository : GenericRepository<Address>, IAddressDal
    {
        public EfAddressRepository(Context.Context context) : base(context)
        {
        }
    }
}
