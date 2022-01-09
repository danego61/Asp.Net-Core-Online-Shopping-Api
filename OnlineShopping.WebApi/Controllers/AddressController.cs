using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Business.Abstract;
using OnlineShopping.Entities.Concrete;
using OnlineShopping.WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopping.WebApi.Controllers
{
    [Route("api/User/{customerID:int}/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly IUserService userService;

        public AddressController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("/api/Cities")]
        public IActionResult GetCities()
        {
            return Ok(userService.GetCities().Select(x => new
            {
                x.CityID,
                x.CityName
            }));
        }

        [HttpGet("/api/Cities/{cityID:int}")]
        public IActionResult GetCityRegions(int cityID)
        {
            return Ok(userService.GetRegions(cityID).Select(x => new
            {
                x.RegionID,
                x.RegionName
            }));
        }

        [HttpGet]
        public IActionResult GetUserAdress(int customerID, string addressName)
        {
            Customer customer = userService.GetCustomer(customerID);
            if (customer != null)
            {
                ICollection<Address> addresses = customer.Addresses;
                if (addressName != null)
                {
                    Address address = addresses.SingleOrDefault(x => x.AddressName == addressName);
                    if (address == null)
                        return BadRequest();
                    return Ok(new AddressModel
                    {
                        AddressDescription = address.AddressDescription,
                        AddressID = address.AddressID,
                        AddressName = address.AddressName,
                        CustomerID = address.CustomerID,
                        RegionID = address.RegionID
                    });
                }
                else
                {
                    return Ok(addresses.Select(address => new AddressModel
                    {
                        AddressDescription = address.AddressDescription,
                        AddressID = address.AddressID,
                        AddressName = address.AddressName,
                        CustomerID = address.CustomerID,
                        RegionID = address.RegionID
                    }));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddAddress(int customerID, AddressModel model)
        {
            Address address = userService.CustomerAddAddress(new Address
            {
                AddressDescription = model.AddressDescription,
                AddressName = model.AddressName,
                RegionID = model.RegionID,
                CustomerID = customerID
            });
            model.AddressID = address.AddressID;
            return CreatedAtAction(nameof(GetUserAdress), new
            {
                customerID = customerID,
                addressName = model.AddressName
            }, model);
        }

        [HttpPut]
        public IActionResult UpdateAddress(int customerID, AddressModel model)
        {
            userService.CustomerUpdateAddress(new Address
            {
                AddressDescription = model.AddressDescription,
                AddressName = model.AddressName,
                RegionID = model.RegionID,
                CustomerID = customerID,
                AddressID = model.AddressID
            });
            return Ok(model);
        }

    }
}
