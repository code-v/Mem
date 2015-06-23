using MVCPlugin.Plugin.Shipping.Yuantong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVCPlugin.Plugin.Shipping.Yuantong.Controllers
{
    public class ShippingYuantongController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            //可以从数据库获取
            YuantongShippingModel model = new YuantongShippingModel();
            model.YuantongShippingFee = 3.1;
            model.YuantongShippingName = "YuantongShipping";
            return View("MVCPlugin.Plugin.Shipping.Yuantong.Views.ShippingYuantong.Index", model);
        }
    }
}
