using Business.Abstract;
using DataAccessLayer.Abstarct;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        public void AddBrand(Brand brand)
        {
            _brandDal.Insert(brand);
        }

        public void DeleteBrand(Brand brand)
        {
            _brandDal.Delete(brand);
        }

        public List<Brand> GetAllBrand(Brand brand)
        {
            return _brandDal.GetListAll();
        }

        public Brand GetById(int id)
        {
            return _brandDal.GetById(id);
        }

        public void UpdateBrand(Brand brand)
        {
            _brandDal.Update(brand);
        }
    }
}
