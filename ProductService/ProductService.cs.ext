using SharedMicroServiceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProductService
{
    [Authorize]
    public class ProductController : ApiController
    {
        private BONorthWind bo = null;

        public ProductController()
        {
            bo = new BONorthWind();
        }

        [Route("api/Product")]
        public HttpResponseMessage GetAllProducts()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            return Request.CreateResponse(HttpStatusCode.OK, bo.GetAllProducts());
        }
        [Route("api/Product/GetProductByProductID/productID")]
        public HttpResponseMessage GetProductByProductID(string productID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (string.IsNullOrEmpty(productID))
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                var lstResult = bo.GetProductByProductID(Convert.ToInt32(productID));
                response = Request.CreateResponse(HttpStatusCode.OK, lstResult);
            }

            return response;
        }
    }
}
