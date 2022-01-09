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
    public class EfEmployeeDal : EfEntityRepositoryBase<Employee>, IEmployeeDal
    {

        public EfEmployeeDal(OnlineShoppingDbContext context) : base(context)
        {

        }

    }
}
