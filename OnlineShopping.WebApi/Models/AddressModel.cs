using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Models
{
    public class AddressModel
    {

        public int AddressID { get; set; }

        public string AddressName { get; set; }

        public int RegionID { get; set; }

        public int CustomerID { get; set; }

        public string AddressDescription { get; set; }

    }
}
