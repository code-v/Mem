using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Hosting;
using System.Web.Routing;
using System.Data.Entity;
using Mem.Core;
using Autofac;
using Autofac.Integration.Mvc;
using Mem.Plugin.Web.EmbeddedViews;
using Mem.Plugin.Web.Infrastructure;
using Mem.Core.Data;
using Mem.Data;

//using Mem.Infrastructure;
//创建时间： 2013-9-26
//创建人：谢锐
//描述： 全局加载

namespace Memory
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            );

        }

        protected void Application_Start()
        {
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
            //Database.SetInitializer<MemoryEntities>(null); 
            //AreaRegistration.RegisterAllAreas();
            //RegisterGlobalFilters(GlobalFilters.Filters);

            //加载插件
            AppDomainTypeFinder typeFinder = new AppDomainTypeFinder();
            var embeddedViewResolver = new EmbeddedViewResolver(typeFinder);
            var embeddedProvider = new EmbeddedViewVirtualPathProvider(embeddedViewResolver.GetEmbeddedViews());
            HostingEnvironment.RegisterVirtualPathProvider(embeddedProvider);

            //群体注册服务，所有的继承IDependency接口的类都将被注册
            var builder = RegisterService();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterRoutes(RouteTable.Routes);
            AuthConfig.RegisterAuth();
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <returns></returns>
        private ContainerBuilder RegisterService()
        {
            var builder = new ContainerBuilder();


            var baseType = typeof(IDependency);
            //var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();
            //此局代替上面的语句，因为线程池回收，注册会失败
            var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            var dataAccess = Assembly.GetExecutingAssembly();

            var AllServices = assemblys
                .SelectMany(s => s.GetTypes())
                .Where(p => baseType.IsAssignableFrom(p) && p != baseType);
            //注入加载所有的继承了IDependency接口的类
            builder.RegisterControllers(assemblys.ToArray());

            builder.RegisterAssemblyTypes(assemblys.ToArray())
                   .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                   .AsImplementedInterfaces().InstancePerLifetimeScope();
            //泛型单独注册
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();
            return builder;
        }

    }
}