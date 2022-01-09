using OnlineShopping.Business.Abstract;
using OnlineShopping.Business.ValidationRules.FluentValidation;
using OnlineShopping.Core.Aspects.Postsharp.AuthorizationAspects;
using OnlineShopping.Core.Aspects.Postsharp.ValidationAspects;
using OnlineShopping.Core.CrossCuttingConcerns.Email;
using OnlineShopping.Core.Utils;
using OnlineShopping.DataAccess.Abstract;
using OnlineShopping.Entities;
using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {

        private readonly ICustomerDal customerDal;

        private readonly IEmployeeDal employeeDal;

        public UserManager(ICustomerDal customerDal, IEmployeeDal employeeDal)
        {
            this.customerDal = customerDal;
            this.employeeDal = employeeDal;
        }

        [FluentValidationAspect(typeof(CustomerValidator))]
        public Customer CreateCustomer(Customer customer)
        {
            customer.Password = PasswordHashUtils.GetPasswordHash(customer.Password);
            customerDal.Add(customer);
            customer.EmailVerifyToken = Guid.NewGuid();
            customerDal.SaveChanges();
            SendVerifyEmail(customer);
            return customer;
        }

        public bool VerifyUserEmail(Guid token)
        {
            Customer user = customerDal.Get(x => x.EmailVerifyToken == token);
            if (user == null)
                return false;
            user.EmailVerifyToken = null;
            user.IsEmailVerified = true;
            customerDal.Update(user);
            customerDal.SaveChanges();
            return true;
        }

        private void SendVerifyEmail(Customer user)
        {
            SendEmail.Send(user.Email, "Online Shopping Account Verification", $"<a href='http://localhost:5000/api/User/Verify/{user.EmailVerifyToken}'>Click here</a><span> and verify account! Your Guid: {user.EmailVerifyToken}</span>");
        }

        [SecuredOperation("Admin")]
        public Employee CreateEmployee(Employee employee)
        {
            employee.Password = PasswordHashUtils.GetPasswordHash(employee.Password);
            employeeDal.Add(employee);
            customerDal.SaveChanges();
            return employee;
        }

        [SecuredOperation("Customer", PropertyName = nameof(Address.CustomerID))]
        public Address CustomerAddAddress(Address address)
        {
            Customer customer = GetCustomer(address.CustomerID);
            customer.Addresses.Add(address);
            customerDal.SaveChanges();
            return address;
        }

        [SecuredOperation("Customer", PropertyName = nameof(CreditCard.CustomerID))]
        public CreditCard CustomerAddCreditCard(CreditCard creditCard)
        {
            Customer customer = GetCustomer(creditCard.CustomerID);
            customer.CreditCards.Add(creditCard);
            customerDal.SaveChanges();
            return creditCard;
        }

        [SecuredOperation("Customer", PropertyName = nameof(Order.CustomerID))]
        public Order CustomerAddOrder(Order order)
        {
            Customer customer = GetCustomer(order.CustomerID);
            customer.Orders.Add(order);
            customerDal.SaveChanges();
            return order;
        }

        [SecuredOperation("Customer", PropertyName = nameof(Address.CustomerID))]
        public Address CustomerUpdateAddress(Address newAddress)
        {
            Customer customer = GetCustomer(newAddress.CustomerID);
            Address address = customer.Addresses.SingleOrDefault(x => x.AddressID == newAddress.AddressID);
            if (address != null)
            {
                address.AddressDescription = newAddress.AddressDescription;
                address.AddressName = newAddress.AddressName;
                address.RegionID = newAddress.RegionID;
                customerDal.SaveChanges();
            }
            return newAddress;
        }

        [SecuredOperation("Customer", PropertyName = nameof(CreditCard.CustomerID))]
        public CreditCard CustomerUpdateCreditCard(CreditCard creditCard)
        {
            Customer customer = GetCustomer(creditCard.CustomerID);
            CreditCard card = customer.CreditCards.SingleOrDefault(x => x.CreditCardID == creditCard.CreditCardID);
            if (card != null)
            {
                card.CreditCardName = creditCard.CreditCardName;
                card.CreditCardNo = creditCard.CreditCardNo;
                card.CVC = creditCard.CVC;
                card.ExpireDateMounth = creditCard.ExpireDateMounth;
                card.ExpireDateYear = creditCard.ExpireDateYear;
                card.NameSurname = creditCard.NameSurname;
                customerDal.SaveChanges();
            }
            return creditCard;
        }

        public List<City> GetCities()
        {
            return customerDal.GetCities();
        }

        public Customer GetCustomer(string email)
        {
            return customerDal.Get(x => x.Email == email);
        }

        [SecuredOperation("Customer", "Admin", PropertyName = "arg")]
        public Customer GetCustomer(int userID)
        {
            return customerDal.Get(x => x.UserID == userID);
        }

        public Employee GetEmployee(string email)
        {
            return employeeDal.Get(x => x.Email == email);
        }

        [SecuredOperation("Customer", "Admin", PropertyName = "arg")]
        public Employee GetEmployee(int userID)
        {
            return employeeDal.Get(x => x.UserID == userID);
        }

        [SecuredOperation("Employee", "Admin")]
        public List<Order> GetNotPreparedOrders()
        {
            return customerDal.GetOrders(x => x.OrderPreparationStatus == OrderPreparationStatus.GettingReady);
        }

        [SecuredOperation("Employee", "Admin")]
        public List<Order> GetNotShippedOrders()
        {
            return customerDal.GetOrders(x => x.OrderShippingStatus != OrderShippingStatus.WasDelivered && x.OrderShippingStatus != OrderShippingStatus.Waiting);
        }

        [SecuredOperation("Employee", "Admin")]
        public List<Order> GetPreparedOrders()
        {
            return customerDal.GetOrders(x => x.OrderPreparationStatus == OrderPreparationStatus.Ready);
        }

        public List<Region> GetRegions(int cityID)
        {
            return customerDal.GetRegions(cityID);
        }

        [SecuredOperation("Employee", "Admin")]
        public List<Order> GetShippedOrders()
        {
            return customerDal.GetOrders(x => x.OrderShippingStatus == OrderShippingStatus.WasDelivered);
        }

        [SecuredOperation("Employee", "Admin")]
        public Order SetOrderPreparedStatus(int orderID, OrderPreparationStatus newStatus)
        {
            Order order = customerDal.GetOrder(orderID);
            switch (newStatus)
            {
                case OrderPreparationStatus.GettingReady:
                    order.OrderShippingStatus = OrderShippingStatus.Waiting;
                    order.ShippedDate = null;
                    break;
                case OrderPreparationStatus.Ready:
                    order.OrderShippingStatus = OrderShippingStatus.WaitingForShipping;
                    break;
            }
            order.OrderPreparationStatus = newStatus;
            customerDal.SaveChanges();
            return order;
        }

        [SecuredOperation("Employee", "Admin")]
        public Order SetOrderShippingStatus(int orderID, OrderShippingStatus newStatus)
        {
            Order order = customerDal.GetOrder(orderID);
            order.ShippedDate = newStatus == OrderShippingStatus.WasDelivered ? DateTime.Now : null;
            order.OrderShippingStatus = newStatus;
            customerDal.SaveChanges();
            return order;
        }

        [SecuredOperation("Customer", "Admin", PropertyName = nameof(Customer.UserID))]
        public Customer UpdateCustomer(Customer customer)
        {
            customer.Password = PasswordHashUtils.GetPasswordHash(customer.Password);
            customerDal.Update(customer);
            customerDal.SaveChanges();
            return customer;
        }

        [SecuredOperation("Employee", "Admin", PropertyName = nameof(Employee.UserID))]
        public Employee UpdateEmployee(Employee employee)
        {
            Employee currentEmp = GetEmployee(employee.UserID);
            employee.Password = PasswordHashUtils.GetPasswordHash(employee.Password);
            if (currentEmp.Password != employee.Password)
                currentEmp.IsPasswordChange = false;
            currentEmp.Name = employee.Name;
            currentEmp.Surname = employee.Surname;
            currentEmp.EmployeeStatus = employee.EmployeeStatus;
            currentEmp.Password = employee.Password;
            employeeDal.Update(currentEmp);
            customerDal.SaveChanges();
            return employee;
        }

    }
}
