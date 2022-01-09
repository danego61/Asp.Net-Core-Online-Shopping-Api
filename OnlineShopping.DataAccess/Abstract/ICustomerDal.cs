using OnlineShopping.Core.DataAccess;
using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {

        List<City> GetCities();

        List<Region> GetRegions(int cityID);

        Order GetOrder(int orderID);

        List<Order> GetOrders(Expression<Func<Order, bool>> expression);

    }
}
