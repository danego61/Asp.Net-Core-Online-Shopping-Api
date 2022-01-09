using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Models
{
    public class ProductModel
    {

        public int ProductID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public string Explanation { get; set; }

        public byte[] Image { get; set; }

        public bool IsDisabled { get; set; }

        public string CategoryName { get; set; }

        public int CategoryID { get; set; }

    }
}
