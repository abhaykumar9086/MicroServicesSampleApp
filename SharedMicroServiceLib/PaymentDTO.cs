
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMicroServiceLib
{
    public class PaymentDTO
    {
        //creditCardType
        public string cardNumber { get; set; }
        public string expirationDate { get; set; }
        public string CardCode { get; set; }

        //customerAddressType
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string address { get; set; }

        //quantity to be purchased
        public int quantity { get; set; }
        public string CustomerID { get; set; }

        public int OrderID { get; set; }
        //Product Information
        public Product productinfo { get; set; }
    }
}
