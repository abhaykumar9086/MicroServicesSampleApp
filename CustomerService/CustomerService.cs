using SharedMicroServiceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CustomerService
{
    [Authorize]
    public class CustomerController : ApiController
    {
        private BONorthWind bo = new BONorthWind();
        private PagedList<Customer> _pagedCustomers = null;
        private int _pageSize = 8;

        private void InitCustomers()
        {

            if (_pagedCustomers == null)
            {
                var customers = bo.GetCustomers();
                _pagedCustomers = new PagedList<Customer>(customers, _pageSize);
            }

        }
        //
        // GET: /Customer/
        [HttpGet]
        [ResponseType(typeof(List<Customer>))]
        public HttpResponseMessage GetCustomer(int page = 1)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                InitCustomers();
                _pagedCustomers.CurrentPage = page;
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, _pagedCustomers.GetListFromPage(_pagedCustomers.CurrentPage));
            }
            catch
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return httpResponse;
        }

        ////
        //// POST: /Customer/Create
        [HttpPost]
        [ResponseType(typeof(Customer))]
        public HttpResponseMessage CreateCustomer(Customer customer)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (!ModelState.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Created, bo.AddCustomer(customer));
            }
            return response;

        }

        ////
        //// PUT: /Customer/Create
        [HttpPut]
        [Route("api/Customer/{id:int}")]
        [ResponseType(typeof(void))]
        public HttpResponseMessage UpdateCustomer(string customerId, Customer customer)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (!ModelState.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (customerId != customer.CustomerID)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, bo.PutCustomer(customerId, customer));
            }
            return response;
        }

    }
}
