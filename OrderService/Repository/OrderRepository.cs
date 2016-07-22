using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedMicroServiceLib;
using OrderService.Repository.Repositoty.EF;
using OrderService.Repository.Interface;
namespace OrderService.Repository
{
    public class OrderRepository : GenericRepository<NorthWindEntities, Order>, IOrderRepository
    {

        #region IOrderRepository Members

        public Order GetSingle(int orderID)
        {
            var query = GetAll().FirstOrDefault(x => x.OrderID == orderID);
            return query;
        }

        public int AddOrder(OrderDetailsDTO orderdetailsDTO)
        {
            //Save Orders
            Context.Orders.Add(new Order
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
            Context.SaveChanges();

            var orderDetails = Context.Orders.Local[0];

            //Save OrderDetails
            Context.Order_Details.Add(new Order_Detail
            {
                OrderID = orderDetails.OrderID,
                ProductID = orderdetailsDTO.ProductID,
                UnitPrice = orderdetailsDTO.UnitPrice,
                Quantity = orderdetailsDTO.Quantity,
                Discount = orderdetailsDTO.Discount
            });
            Context.SaveChanges();
            return orderDetails.OrderID;
        }

        public bool OrderExists(int orderID)
        {
            return (Context.Orders.Count(x => x.OrderID == orderID) > 0);
        }

        #endregion
    }
}
