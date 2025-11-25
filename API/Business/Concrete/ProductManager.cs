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
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public void AddProduct(Product product)
        {
            _productDal.Insert(product);
        }

        public void DeleteProduct(Product product)
        {
            _productDal.Delete(product);
        }

        public List<Product> GetAllProduct()
        {
            return _productDal.GetListAll();
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public void UpdateProduct(Product product)
        {
            _productDal.Update(product);
        }
    }
}
