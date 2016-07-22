using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedMicroServiceLib;

namespace OrderService.Repository.Interface
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Order GetSingle(int orderID);
        bool OrderExists(int orderID);
    }
}
