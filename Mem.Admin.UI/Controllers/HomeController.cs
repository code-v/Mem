using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Ajax;

using Mem.Service.Index;
using Mem.Plugin;
using Mem.Web.Models;
using AdminMemory.Models.Catalog;

namespace Memory.Controllers
{
    public class HomeController : Controller
    {
        #region 对象
        private readonly IIndexService _indexService;
        #endregion

        #region 构造函数
        public HomeController(IIndexService indexService)
        {
            this._indexService = indexService;
        }
        #endregion

        #region 方法

        public ActionResult Index()
        {
            //加载插件
            PluginFinder _pluginFinder = new PluginFinder();
            List<IShippingMethod> shippingMethodList = _pluginFinder.GetPlugins<IShippingMethod>().ToList();
            ViewData["ShippingMethods"] = shippingMethodList;
            _indexService.GetTestName();
            var TestTable = _indexService.GetTestTable();
            var categoryModelList =
               TestTable.Select(p =>
               {
                   var categoryModel = new CategoryModel();
                   categoryModel.Id = p.Id;
                   categoryModel.Name = p.Name;
                   return categoryModel;
               });
            IEnumerable<CategoryModel> CategoryModel = categoryModelList;
            return View(categoryModelList);
        }

        public ActionResult ShippingMethod(string method)
        {
            PluginFinder _pluginFinder = new PluginFinder();
            var pluginDescriptor = _pluginFinder.GetPluginDescriptorBySystemName<IShippingMethod>(method);
            var plugin = pluginDescriptor.Instance() as IShippingMethod;

            string actionName = "";
            string controllerName = "";
            RouteValueDictionary configurationRouteValues = null;
            plugin.GetConfigurationRoute(out actionName, out controllerName, out configurationRouteValues);

            ShippingMethodModel model = new ShippingMethodModel()
            {
                ActionName = actionName,
                ConfigurationRouteValues = configurationRouteValues,
                ControllerName = controllerName
            };

            return View(model);
        }


        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(CategoryModel model)
        {
            return View();
        }

        public ActionResult MyTest()
        {
            return PartialView();
        }

        #endregion

    }
}
