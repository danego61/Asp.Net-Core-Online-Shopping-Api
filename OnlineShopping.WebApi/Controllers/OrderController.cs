using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Business.Abstract;
using OnlineShopping.Entities.Concrete;
using OnlineShopping.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopping.WebApi.Controllers
{
    [Route("api/User/{customerID:int}/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IUserService userService;

        public OrderController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{orderID:int?}")]
        public IActionResult GetOrder(int customerID, int? orderID)
        {
            Customer customer = userService.GetCustomer(customerID);
            if (customer != null)
            {
                ICollection<Order> orders = customer.Orders;
                if (orderID != null)
                {
                    Order order = orders.SingleOrDefault(x => x.OrderID == orderID);
                    if (order == null)
                        return NotFound();
                    return Ok(OrderConvert(order));
                }
                else
                {
                    return Ok(OrderConvert(orders));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddOrder(int customerID, NewOrderModel model, [FromServices] IProductService productService)
        {
            Customer customer = userService.GetCustomer(customerID);
            if (customer == null)
                return BadRequest();
            CreditCard creditCard = customer.CreditCards.SingleOrDefault(x => x.CreditCardID == model.CreditCardID);
            Address address = customer.Addresses.SingleOrDefault(x => x.AddressID == model.AddressID);
            if (address == null || creditCard == null)
                return BadRequest();
            Order order = new()
            {
                CustomerID = customerID,
                ShipRegion = address.Region.RegionName,
                ShipCity = address.Region.City.CityName,
                OrderDate = DateTime.Now,
                ShipAddress = address.AddressDescription,
                CreditCardNo = creditCard.CreditCardNo,
                OrderPreparationStatus = Entities.OrderPreparationStatus.GettingReady,
                OrderShippingStatus = Entities.OrderShippingStatus.Waiting
            };
            order.OrderDetails = model.OrderDetails.Select(x =>
            {
                Product product = productService.GetProduct(x.ProductID);
                return new OrderDetails
                {
                    Order = order,
                    ProductID = product.ProductID,
                    UnitPrice = product.Price,
                    Quantity = x.Quantity
                };
            }).ToHashSet();
            userService.CustomerAddOrder(order);
            return CreatedAtAction(nameof(GetOrder), new
            {
                customerID = customerID,
                orderID = order.OrderID
            }, OrderConvert(order));
        }

        [HttpGet("/api/Order/NotPrepared")]
        public IActionResult GetNotPreparedOrders()
        {
            return Ok(OrderConvert(userService.GetNotPreparedOrders()));
        }

        [HttpGet("/api/Order/Prepared")]
        public IActionResult GetPreparedOrders()
        {
            return Ok(OrderConvert(userService.GetPreparedOrders()));
        }

        [HttpPatch("/api/Order/{orderID:int}")]
        public IActionResult OrderSetPreparedStatus(int orderID, OrderSetStatusModel model)
        {
            if (Enum.TryParse(model.PreparationStatus, out Entities.OrderPreparationStatus preparation))
                return Ok(OrderConvert(userService.SetOrderPreparedStatus(orderID, preparation)));
            if (Enum.TryParse(model.ShippingStatus, out Entities.OrderShippingStatus shipping))
                return Ok(OrderConvert(userService.SetOrderShippingStatus(orderID, shipping)));
            return BadRequest();
        }

        [HttpGet("/api/Order/NotShipped")]
        public IActionResult GetNotShippedOrders()
        {
            return Ok(OrderConvert(userService.GetNotShippedOrders()));
        }

        [HttpGet("/api/Order/Shipped")]
        public IActionResult GetShippedOrders()
        {
            return Ok(OrderConvert(userService.GetShippedOrders()));
        }

        private OrderModel OrderConvert(Order order)
        {
            return new OrderModel
            {
                OrderID = order.OrderID,
                CreditCardNo = order.CreditCardNo,
                CustomerID = order.CustomerID,
                OrderDate = order.OrderDate,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShippedDate = order.ShippedDate,
                ShipRegion = order.ShipRegion,
                OrderPreparationStatus = order.OrderPreparationStatus.ToString(),
                OrderShippingStatus = order.OrderShippingStatus.ToString(),
                OrderDetails = order.OrderDetails.Select(x => new OrderDetailsModel
                {
                    ProductID = x.ProductID,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToArray()
            };
        }

        private ICollection<OrderModel> OrderConvert(ICollection<Order> order)
        {
            return order.Select(order => new OrderModel
            {
                OrderID = order.OrderID,
                CreditCardNo = order.CreditCardNo,
                CustomerID = order.CustomerID,
                OrderDate = order.OrderDate,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShippedDate = order.ShippedDate,
                ShipRegion = order.ShipRegion,
                OrderPreparationStatus = order.OrderPreparationStatus.ToString(),
                OrderShippingStatus = order.OrderShippingStatus.ToString(),
                OrderDetails = order.OrderDetails.Select(x => new OrderDetailsModel
                {
                    ProductID = x.ProductID,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToArray()
            }).ToHashSet();
        }

    }
}
