using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Models
{
    public class CategoryModel
    {

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public bool IsDisabled { get; set; }

    }
}
