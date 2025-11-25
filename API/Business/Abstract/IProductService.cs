using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAllProduct();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

        Product GetById(int id);
    }
}
