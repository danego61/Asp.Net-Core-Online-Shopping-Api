using OnlineShopping.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities.Concrete
{
    public class CreditCard : IEntity
    {

        public int CreditCardID { get; set; }

        public string CreditCardName { get; set; }

        public string NameSurname { get; set; }

        public string CreditCardNo { get; set; }

        public int ExpireDateYear { get; set; }

        public int ExpireDateMounth { get; set; }

        public int CVC { get; set; }

        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

    }
}
