using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FulfillmentService
{
    [Authorize]
    public class FulfillmentService
    {
        [HttpGet]
        public bool GetFullFillment(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
