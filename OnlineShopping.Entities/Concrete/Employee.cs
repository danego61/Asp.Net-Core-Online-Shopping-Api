using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities.Concrete
{
    public class Employee : User
    {

        public EmployeeStatus EmployeeStatus { get; set; }

        public EmployeeType EmployeeType { get; set; }

    }
}
