using OnlineShopping.Entities.Concrete;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Abstract
{
    public interface IUserService
    {

        Employee GetEmployee(string email);

        Employee GetEmployee(int userID);

        Employee CreateEmployee(Employee employee);

        Employee UpdateEmployee(Employee employee);

        Customer GetCustomer(string email);

        Customer GetCustomer(int userID);

        bool VerifyUserEmail(Guid token);

        Customer CreateCustomer(Customer customer);

        Customer UpdateCustomer(Customer customer);

        CreditCard CustomerAddCreditCard(CreditCard creditCard);

        CreditCard CustomerUpdateCreditCard(CreditCard creditCard);

        Address CustomerAddAddress(Address address);

        Address CustomerUpdateAddress(Address address);

        Order CustomerAddOrder(Order order);

        List<Order> GetNotPreparedOrders();

        List<Order> GetPreparedOrders();

        Order SetOrderPreparedStatus(int orderID, OrderPreparationStatus newStatus);

        List<Order> GetNotShippedOrders();

        List<Order> GetShippedOrders();

        Order SetOrderShippingStatus(int orderID, OrderShippingStatus newStatus);

        List<City> GetCities();

        List<Region> GetRegions(int cityID);

    }
}
