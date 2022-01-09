using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Models
{
    public class NewOrderModel
    {

        public int CustomerID { get; set; }

        public int CreditCardID { get; set; }

        public int AddressID { get; set; }

        public ICollection<OrderDetailsModel> OrderDetails { get; set; }

    }
}
