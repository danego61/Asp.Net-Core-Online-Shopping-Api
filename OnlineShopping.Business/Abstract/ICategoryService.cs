using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Abstract
{
    public interface ICategoryService
    {

        Category AddCategory(Category category);

        Category UpdateCategory(Category category);

        Category GetCategory(string categoryName);

        List<Category> GetAllCategories(bool withDisabled = false);

    }
}
