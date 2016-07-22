using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SharedMicroServiceLib;
using System.Net;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

namespace OrderService
{
    [Authorize]
    public class OrderController : ApiController
    {

        private BONorthWind bo = new BONorthWind();
        private PagedList<Order> _pagedOrders = null;
        private int _pageSize = 8;


        private readonly string accessToken = "accessToken";
        private readonly string OrderServiceHostUrl = "OrderService.Host";

        public OrderController()
        {
            accessToken = ConfigurationManager.AppSettings["accessToken"];
            OrderServiceHostUrl = ConfigurationManager.AppSettings["OrderService.Host"];
        }

        private void InitOrders(string id)
        {
            var orders = bo.GetOrders(id);
            _pagedOrders = new PagedList<Order>(orders, _pageSize);

        }
        //
        // GET: /Order/
        [ResponseType(typeof(List<Order>))]
        [Route("api/Order/GetOrders/{id}")]
        public HttpResponseMessage GetOrders(string id, int page = 1)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (string.IsNullOrEmpty(id))
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else if (_pagedOrders == null)
            {
                InitOrders(id);
                _pagedOrders.CurrentPage = page;
                var lstResult = _pagedOrders.GetListFromPage(_pagedOrders.CurrentPage);
                response = Request.CreateResponse(HttpStatusCode.OK, lstResult);
            }
            return response;
        }


        [Route("api/Order/GetOrderbyCustomerID/{id}")]
        [ResponseType(typeof(List<Order>))]
        public HttpResponseMessage GetOrderbyCustomerID(string id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (string.IsNullOrEmpty(id))
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                var lstResult = bo.GetOrderByCustomerId(id);
                response = Request.CreateResponse(HttpStatusCode.OK, lstResult);
            }

            return response;
        }

        [HttpPost]
        [ResponseType(typeof(Order))]
        public HttpResponseMessage CreateOrder(OrderDetailsDTO orderDetailsDTO, string CustomerID = null, Nullable<int> EmployeeID = null, Nullable<int> ShipVia = null)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (!ModelState.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                var orderid = bo.AddOrder(orderDetailsDTO);
                response = Request.CreateResponse(HttpStatusCode.Created, orderid);
            }
            catch (Exception ex)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("api/Order/GetOrderDetails/{Id}")]
        [ResponseType(typeof(List<Order_Detail>))]
        public HttpResponseMessage GetOrderDetails(string id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, bo.GetOrderDetails(id));
        }

    }


}


