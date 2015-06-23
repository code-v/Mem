using MVCPlugin.Plugin.Shipping.Fedex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVCPlugin.Plugin.Shipping.Fedex.Controllers
{
    public class ShippingFedexController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            //可以从数据库获取
            FedexShippingModel model = new FedexShippingModel();
            model.FedexShippingFee = 1.1;
            model.FedexShippingName = "FedexShipping";
            return View("MVCPlugin.Plugin.Shipping.Fedex.Views.ShippingFedex.Index", model);
        }
    }
}
