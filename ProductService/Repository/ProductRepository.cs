using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedMicroServiceLib;
using ProductService.Repository.Repositoty.EF;
using ProductService.Repository.Interface;

namespace ProductService.Repository
{
    public class ProductRepository : GenericRepository<NorthWindEntities, Product>, IProductRepository
    {

        #region IProductRepository Members

        public Product GetSingle(int productID)
        {
            var query = GetAll().FirstOrDefault(x => x.ProductID == productID);
            return query;
        }

        public bool ProductExists(int productID)
        {
            return (Context.Products.Count(x => x.ProductID == productID) > 0);
        }

        #endregion
    }
}
