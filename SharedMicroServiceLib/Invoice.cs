using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SharedMicroServiceLib
{
    public class Invoice : InvoiceElement
    {

        private List<InvoiceElement> _invoiceElements;
        public Invoice(Order o)
        {

            InitInvoice(o);
        }

        private HeaderVM InitHeader()
        {
            HeaderVM vm = new HeaderVM();
            vm.Name = "ABC PLC";
            vm.Address1 = "121,J.B.Road";
            vm.Address2 = "Fleet Street";
            vm.City = "London";
            vm.Zip = "LS0 5TQ";
            vm.Country = "UK";
            vm.Email = "sales@abcplcltd.com";
            vm.Website = "http://www.abcplc.co.uk";
            return vm;
        }
        private void InitInvoice(Order o)
        {
            _invoiceElements = new List<InvoiceElement>();
            HeaderElement header = new HeaderElement(InitHeader());
            _invoiceElements.Add(header);
            OrderElement orderElemt = new OrderElement(o);
            _invoiceElements.Add(orderElemt);
            FooterElement footElement = new FooterElement("Thank you for doing Business with us!");
            _invoiceElements.Add(footElement);
        }

        public void accept(IVisitor visitor)
        {

            _invoiceElements.ForEach(ie => ie.accept(visitor));
        }
    }



    public class HeaderElement : InvoiceElement
    {
        HeaderVM _header;

        public HeaderVM Header { get { return _header; } }

        public HeaderElement(HeaderVM header)
        {
            _header = header;
        }
        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }
    }



    public class OrderElement : InvoiceElement
    {

        Order _order;
        public Order CurrentOrder { get { return _order; } }
        public OrderElement(Order o)
        {

            _order = o;
        }

        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }
    }

    public class FooterElement : InvoiceElement
    {
        string _footer;

        public string Footer { get { return _footer; } }
        public FooterElement(string footer)
        {
            _footer = footer;
        }
        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }
    }

}