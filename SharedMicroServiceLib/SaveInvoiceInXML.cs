using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
namespace SharedMicroServiceLib
{
    public class SaveInvoiceInXML : IVisitor
    {
        private XElement _root = new XElement("invoice");
        public void visit(InvoiceElement invoiceElement)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Method:visit
        /// Purpose:save headerElement in XML format.
        /// </summary>
        /// <param name="headerElement"></param>
        public void visit(HeaderElement headerElement)
        {
            _root.Add(new XElement("header",
                            new XElement("company", headerElement.Header.Name),
                            new XElement("address1", headerElement.Header.Address1),
                            new XElement("address2", headerElement.Header.Address1),
                            new XElement("city", headerElement.Header.City),
                            new XElement("country", headerElement.Header.Country),
                            new XElement("zip", headerElement.Header.Zip),
                            new XElement("email", headerElement.Header.Email),
                            new XElement("website", headerElement.Header.Website)

                        ));
        }

        /// <summary>
        /// Method:visit
        /// purpose:Saves the order content in XML format.
        /// </summary>
        /// <param name="orderElement"></param>

        public void visit(OrderElement orderElement)
        {
            // Add order and orderElement

            Decimal orderAmount = orderElement.CurrentOrder.Order_Details.Sum(odet => (odet.UnitPrice * odet.Quantity));
            Decimal taxAmount = 0.20M * orderAmount;
            orderAmount += taxAmount;
            _root.Add(new XElement("order",
                       new XAttribute("orderid", orderElement.CurrentOrder.OrderID),
                       new XElement("orderdate", string.Format("{0:d}", orderElement.CurrentOrder.OrderDate)),
                       new XElement("shipping_details",
                           new XElement("contact_person", orderElement.CurrentOrder.Customer.ContactName),
                           new XElement("company", orderElement.CurrentOrder.Customer.CompanyName),
                           new XElement("address", orderElement.CurrentOrder.ShipAddress),
                           new XElement("city", orderElement.CurrentOrder.ShipCity),
                           new XElement("zip", orderElement.CurrentOrder.ShipPostalCode),
                           new XElement("country", orderElement.CurrentOrder.ShipCountry)
                           ),
                        new XElement("orderdetails",
                            from odet in orderElement.CurrentOrder.Order_Details
                            select new XElement("order_detail",
                                new XElement("productid", odet.ProductID),
                                new XElement("description", odet.Product.ProductName),
                                new XElement("unitprice", odet.UnitPrice),
                                new XElement("quantity", odet.Quantity)
                                )
                            ),
                        new XElement("vat", string.Format("{0:0.00}", taxAmount)),
                        new XElement("totalamount", string.Format("{0:0.00}", orderAmount))

                ));
        }

        /// <summary>
        /// Method:visit
        /// Purpose:Saves footer.
        /// </summary>
        /// <param name="footerElement"></param>
        public void visit(FooterElement footerElement)
        {
            _root.Add(new XElement("footer", footerElement.Footer));
        }

        public XElement InvoiceRoot { get { return _root; } }
    }
}