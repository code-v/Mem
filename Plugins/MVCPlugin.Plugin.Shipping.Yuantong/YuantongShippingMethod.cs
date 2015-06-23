using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Mem.Plugin;
namespace MVCPlugin.Plugin.Shipping.Yuantong
{
    public class YuantongShippingMethod : BasePlugin, IShippingMethod
    {
        public void GetConfigurationRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            actionName = "Index";
            controllerName = "ShippingYuantong";
            routeValues = new RouteValueDictionary() { { "Namespaces", "MVCPlugin.Plugin.Shipping.Yuantong.Controllers" }, { "area", null } };
        }
    }
}
