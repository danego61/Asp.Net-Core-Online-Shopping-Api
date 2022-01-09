using OnlineShopping.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities.Concrete
{
    public class Address : IEntity
    {

        public int AddressID { get; set; }

        public string AddressName { get; set; }

        public int RegionID { get; set; }

        public int CustomerID { get; set; }

        public string AddressDescription { get; set; }

        public virtual Region Region { get; set; }

        public virtual Customer Customer { get; set; }

    }
}
