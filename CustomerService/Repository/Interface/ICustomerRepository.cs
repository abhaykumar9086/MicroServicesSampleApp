using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedMicroServiceLib;

namespace CustomerService.Repository.Interface
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Customer GetSingle(string customerID);
        bool CustomerExists(string id);
    }
}
