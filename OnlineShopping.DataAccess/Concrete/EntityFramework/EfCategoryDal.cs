using OnlineShopping.Core.DataAccess.EntityFramework;
using OnlineShopping.DataAccess.Abstract;
using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category>, ICategoryDal
    {

        public EfCategoryDal(OnlineShoppingDbContext context) : base(context)
        {

        }

    }
}
