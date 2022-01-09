using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Models
{
    public class OrderModel
    {

        public int OrderID { get; set; }

        public int CustomerID { get; set; }

        public string CreditCardNo { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipAddress { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public string OrderPreparationStatus { get; set; }

        public string OrderShippingStatus { get; set; }

        public ICollection<OrderDetailsModel> OrderDetails { get; set; }

    }
}
