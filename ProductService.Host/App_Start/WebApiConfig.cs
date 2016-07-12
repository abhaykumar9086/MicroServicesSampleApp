using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ProductService.Host
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //api/Product/GetProductByProductID/{ID}

            config.Routes.MapHttpRoute(
             name: "GetProductByProductID",
             routeTemplate: "api/{controller}/{ation}/{productID}",
             defaults: new { productID = RouteParameter.Optional }
         );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
