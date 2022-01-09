using OnlineShopping.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities.Concrete
{
    public class Order : IEntity
    {

        public int OrderID { get; set; }

        public int CustomerID { get; set; }

        public string CreditCardNo { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipAddress { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public OrderPreparationStatus OrderPreparationStatus { get; set; }

        public OrderShippingStatus OrderShippingStatus { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public virtual Customer Customer { get; set; }

        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

    }
}
