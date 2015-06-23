using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Mem.Plugin;

namespace MVCPlugin.Plugin.Shipping.Fedex
{
    public class FedexShippingMethod : BasePlugin, IShippingMethod
    {
        public void GetConfigurationRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            actionName = "Index";
            controllerName = "ShippingFedex";
            routeValues = new RouteValueDictionary() { { "Namespaces", "MVCPlugin.Plugin.Shipping.Fedex.Controllers" }, { "area", null } };
        }
    }
}
