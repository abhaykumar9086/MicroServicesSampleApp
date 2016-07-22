using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OrderService.Repository;
using SharedMicroServiceLib;


namespace OrderService
{
    public class OrderController : ApiController
    {
        private OrderRepository _orderRep = new OrderRepository();

        // GET: api/Order
        public IQueryable<Order> GetOrders()
        {
            return _orderRep.GetAll();
        }

        // GET: api/Order/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = _orderRep.GetSingle(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Order/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _orderRep.Edit(order);

            try
            {
                _orderRep.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Order
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(OrderDetailsDTO orderDetailsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderid = _orderRep.AddOrder(orderDetailsDTO);
            _orderRep.Save();

            return CreatedAtRoute("DefaultApi", new { id = orderid }, orderDetailsDTO);
        }

        // DELETE: api/Order/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = _orderRep.GetSingle(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderRep.Delete(order);
            _orderRep.Save();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _orderRep.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return _orderRep.OrderExists(id);
        }
    }
}
