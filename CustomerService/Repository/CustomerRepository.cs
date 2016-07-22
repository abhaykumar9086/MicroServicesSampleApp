using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedMicroServiceLib;
using CustomerService.Repository.Repositoty.EF;
using CustomerService.Repository.Interface;
namespace CustomerService.Repository
{
    public class CustomerRepository : GenericRepository<NorthWindEntities, Customer>, ICustomerRepository
    {

        #region ICustomerRepository Members

        public Customer GetSingle(string customerID)
        {
            var query = GetAll().FirstOrDefault(x => x.CustomerID == customerID);
            return query;
        }

        #endregion

        #region ICustomerRepository Members

        public bool CustomerExists(string id)
        {
            return (Context.Customers.Count(x => x.CustomerID == id) > 0);
        }

        #endregion
    }
}
