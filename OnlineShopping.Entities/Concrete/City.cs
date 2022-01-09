using OnlineShopping.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities.Concrete
{
    public class City : IEntity
    {

        public int CityID { get; set; }

        public string CityName { get; set; }

        public virtual ICollection<Region> Regions { get; set; }

        public City()
        {
            Regions = new HashSet<Region>();
        }

    }
}
