using OnlineShopping.Business.Abstract;
using OnlineShopping.Core.Aspects.Postsharp.AuthorizationAspects;
using OnlineShopping.DataAccess.Abstract;
using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Concrete.Managers
{
    public class CategoryManager : ICategoryService
    {

        private readonly ICategoryDal categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            this.categoryDal = categoryDal;
        }

        [SecuredOperation("Admin")]
        public Category AddCategory(Category category)
        {
            categoryDal.Add(category);
            categoryDal.SaveChanges();
            return category;
        }

        public List<Category> GetAllCategories(bool withDisabled = false)
        {
            return withDisabled ? categoryDal.GetList() : categoryDal.GetList(x => x.CategoryStatus != Entities.CategoryStatus.Deleted);
        }

        public Category GetCategory(string categoryName)
        {
            return categoryDal.Get(x => x.CategoryName == categoryName);
        }

        [SecuredOperation("Admin")]
        public Category UpdateCategory(Category category)
        {
            Category cat = categoryDal.Get(x => x.CategoryID == category.CategoryID);
            cat.CategoryName = category.CategoryName;
            cat.CategoryStatus = category.CategoryStatus;
            categoryDal.Update(cat);
            categoryDal.SaveChanges();
            return category;
        }

    }
}
