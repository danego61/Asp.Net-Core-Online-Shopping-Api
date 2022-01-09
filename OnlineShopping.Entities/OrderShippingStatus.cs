using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Entities
{
    public enum OrderShippingStatus
    {

        Waiting,

        WaitingForShipping,

        Shipping,

        WasDelivered,

        WrongAddress,

        CustomerWasNotAtAddress

    }
}
