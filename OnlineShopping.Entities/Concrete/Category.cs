using OnlineShopping.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities.Concrete
{
    public class Category : IEntity
    {

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public CategoryStatus CategoryStatus { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new HashSet<Product>();
        }

    }
}
