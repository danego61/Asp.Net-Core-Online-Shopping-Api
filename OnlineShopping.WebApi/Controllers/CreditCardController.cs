using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Business.Abstract;
using OnlineShopping.Entities.Concrete;
using OnlineShopping.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Controllers
{
    [Route("api/User/{customerID:int}/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {

        private readonly IUserService userService;

        public CreditCardController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetCustomerCard(int customerID, string cardName)
        {
            Customer customer = userService.GetCustomer(customerID);
            if (customer != null)
            {
                ICollection<CreditCard> creditCards = customer.CreditCards;
                if (cardName != null)
                {
                    CreditCard creditCard = creditCards.SingleOrDefault(x => x.CreditCardName == cardName);
                    if (creditCard == null)
                        return BadRequest();
                    return Ok(new CreditCardModel
                    {
                        CreditCardID = creditCard.CreditCardID,
                        CreditCardName = creditCard.CreditCardName,
                        CreditCardNo = creditCard.CreditCardNo,
                        CVC = creditCard.CVC,
                        ExpireDateMounth = creditCard.ExpireDateMounth,
                        ExpireDateYear = creditCard.ExpireDateYear,
                        NameSurname = creditCard.NameSurname,
                        CustomerID = creditCard.CustomerID
                    });
                }
                else
                {
                    return Ok(creditCards.Select(creditCard => new CreditCardModel
                    {
                        CreditCardID = creditCard.CreditCardID,
                        CreditCardName = creditCard.CreditCardName,
                        CreditCardNo = creditCard.CreditCardNo,
                        CVC = creditCard.CVC,
                        ExpireDateMounth = creditCard.ExpireDateMounth,
                        ExpireDateYear = creditCard.ExpireDateYear,
                        NameSurname = creditCard.NameSurname,
                        CustomerID = creditCard.CustomerID
                    }));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddCreditCard(int customerID, CreditCardModel model)
        {
            CreditCard creditCard = userService.CustomerAddCreditCard(new CreditCard
            {
                NameSurname = model.NameSurname,
                CreditCardNo = model.CreditCardNo,
                ExpireDateYear = model.ExpireDateYear,
                ExpireDateMounth = model.ExpireDateMounth,
                CVC = model.CVC,
                CreditCardName = model.CreditCardName,
                CustomerID = customerID
            });
            model.CreditCardID = creditCard.CreditCardID;
            return CreatedAtAction(nameof(GetCustomerCard), new
            {
                customerID = customerID,
                cardName = model.CreditCardName
            }, model);
        }

        [HttpPut]
        public IActionResult UpdateCreditCard(int customerID, CreditCardModel model)
        {
            userService.CustomerUpdateCreditCard(new CreditCard
            {
                NameSurname = model.NameSurname,
                CreditCardNo = model.CreditCardNo,
                ExpireDateYear = model.ExpireDateYear,
                ExpireDateMounth = model.ExpireDateMounth,
                CVC = model.CVC,
                CreditCardName = model.CreditCardName,
                CustomerID = customerID,
                CreditCardID = model.CreditCardID
            });
            return Ok(model);
        }

    }
}
