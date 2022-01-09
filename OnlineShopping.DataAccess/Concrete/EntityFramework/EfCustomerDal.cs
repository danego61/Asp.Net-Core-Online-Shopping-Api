using OnlineShopping.Core.DataAccess.EntityFramework;
using OnlineShopping.DataAccess.Abstract;
using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer>, ICustomerDal
    {
        public EfCustomerDal(OnlineShoppingDbContext context) : base(context)
        {

        }

        public List<City> GetCities()
        {
            return _context.Set<City>().ToList();
        }

        public Order GetOrder(int orderID)
        {
            return _context.Set<Order>().Find(orderID);
        }

        public List<Order> GetOrders(Expression<Func<Order, bool>> expression)
        {
            return _context.Set<Order>().Where(expression).ToList();
        }

        public List<Region> GetRegions(int cityID)
        {
            return _context.Set<Region>().Where(x => x.CityID == cityID).ToList();
        }
    }
}
