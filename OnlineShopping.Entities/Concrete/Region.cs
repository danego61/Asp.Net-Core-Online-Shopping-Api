using OnlineShopping.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities.Concrete
{
    public class Region : IEntity
    {

        public int RegionID { get; set; }

        public string RegionName { get; set; }

        public int CityID { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

    }
}
