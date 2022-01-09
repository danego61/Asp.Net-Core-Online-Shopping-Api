using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Models
{
    public class EmployeeModel : UserModel
    {

        public bool IsWorking { get; set; }

        public string EmployeeType { get; set; }

    }
}
