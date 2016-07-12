using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMicroServiceLib
{

    public interface IVisitor
    {

        void visit(InvoiceElement invoiceElement);
        void visit(HeaderElement headerElement);
        void visit(OrderElement orderElement);
        void visit(FooterElement footerElement);
    }
    public interface InvoiceElement
    {
        void accept(IVisitor visitor);
    }


}
