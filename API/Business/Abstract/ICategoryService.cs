using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAllCategory(Category category);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);

        Category GetById(int id);
    }
}
