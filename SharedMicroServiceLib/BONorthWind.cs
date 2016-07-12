using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;

namespace SharedMicroServiceLib
{
    public class BONorthWind
    {
        private NorthWindEntities _dbContext = new NorthWindEntities();


        public Customer AddCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            try
            {
                _dbContext.SaveChanges();
                return customer;
            }
            catch
            {
                //
                throw;
            }

        }

        public HttpStatusCode PutCustomer(string customerId, Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customerId))
                {
                    return HttpStatusCode.NotFound;
                }
                else
                {
                    throw;
                }
            }
            return HttpStatusCode.NoContent;
        }

        private bool CustomerExists(string customerId)
        {
            return _dbContext.Customers.Count(e => e.CustomerID == customerId) > 0;
        }

        public List<Customer> GetCustomers()
        {
            var customers = from c in _dbContext.Customers
                            orderby c.CustomerID
                            select c;

            return customers.ToList<Customer>();
        }

        public List<Order> GetOrders(string customerID)
        {

            return _dbContext.Orders.Where(o => o.CustomerID.Equals(customerID, StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(o => o.OrderDate).ToList<Order>();

        }

        public int AddOrder(OrderDetailsDTO orderdetailsDTO)
        {
            //Save Orders
            _dbContext.Orders.Add(new Order
            {
                CustomerID = orderdetailsDTO.CustomerID,
                EmployeeID = orderdetailsDTO.EmployeeID,
                OrderDate = orderdetailsDTO.OrderDate,
                RequiredDate = orderdetailsDTO.RequiredDate,
                ShippedDate = orderdetailsDTO.ShippedDate,
                ShipVia = orderdetailsDTO.ShipVia,
                Freight = orderdetailsDTO.ShipVia,
                ShipName = orderdetailsDTO.ShipName,
                ShipAddress = orderdetailsDTO.ShipAddress,
                ShipCity = orderdetailsDTO.ShipCity,
                ShipRegion = orderdetailsDTO.ShipRegion,
                ShipPostalCode = orderdetailsDTO.ShipPostalCode,
                ShipCountry = orderdetailsDTO.ShipCountry
            });
            _dbContext.SaveChanges();

            var orderDetails = _dbContext.Orders.Local[0];

            //Save OrderDetails
            _dbContext.Order_Details.Add(new Order_Detail
            {
                OrderID = orderDetails.OrderID,
                ProductID = orderdetailsDTO.ProductID,
                UnitPrice = orderdetailsDTO.UnitPrice,
                Quantity = orderdetailsDTO.Quantity,
                Discount = orderdetailsDTO.Discount
            });
            _dbContext.SaveChanges();
            return orderDetails.OrderID;
        }


        public List<Order> GetOrderByCustomerId(string customerId)
        {
            return _dbContext.Orders.Where(x => x.CustomerID == customerId).Select(x => x).ToList();
        }

        public List<Product> GetProductByProductID(int productID)
        {
            return _dbContext.Products.Where(x => x.ProductID == productID).Select(x => x).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public List<Order_Detail> GetOrderDetails(string orderid)
        {
            Int32 orderidint = Convert.ToInt32(orderid);
            return _dbContext.Order_Details.Where(x => x.OrderID == orderidint).Select(x => x).ToList();
        }
    }
}