using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedMicroServiceLib;

namespace ProductService.Repository.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetSingle(int productID);
        bool ProductExists(int productID);
    }
}
