using OnlineShopping.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities.Concrete
{
    public class Product : IEntity
    {

        public int ProductID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public string Explanation { get; set; }

        public byte[] Image { get; set; }

        public ProductStatus ProductStatus { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetails> ProductOrders { get; set; }

    }
}
