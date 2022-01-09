using Microsoft.EntityFrameworkCore;
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
    public class EfProductDal : EfEntityRepositoryBase<Product>, IProductDal
    {

        public EfProductDal(OnlineShoppingDbContext context) : base(context)
        {

        }

        public List<Product> GetList(string categoryName)
        {
            DbSet<Product> product = _context.Set<Product>();
            var query = from p in product
                        join c in _context.Set<Category>() on p.CategoryID equals c.CategoryID
                        where c.CategoryName == categoryName
                        select p;
            return query.ToList();
        }
    }
}
