using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Mem.Web.Models
{
    public class ShippingMethodModel
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public RouteValueDictionary ConfigurationRouteValues { get; set; }
    }
}