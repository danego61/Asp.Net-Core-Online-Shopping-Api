using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities.Concrete
{
    public class Customer : User
    {

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<CreditCard> CreditCards { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Addresses = new HashSet<Address>();
            CreditCards = new HashSet<CreditCard>();
            Orders = new HashSet<Order>();
        }

    }
}
