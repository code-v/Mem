using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Mem.Data;
using Mem.Core;
using Mem.Service;
using Memory.Controllers;
using Autofac.Core;
using Memory;
using Mem.Core.Data;
namespace Mem.Infrastructure
{
    internal class DependencyConfigure
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            DependencyResolver.SetResolver(
                new Dependency(RegisterServices(builder))
                );
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(
                typeof(MvcApplication).Assembly
                ).PropertiesAutowired();

            //在这里处理需要单独注册的类
            builder.RegisterType<DataContext>().As<IDbContext>().InstancePerDependency();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));

            //builder.RegisterType<ProductService>().As<IProductService>();
            //builder.RegisterType<CategoryService>().As<ICategoryService>();


            return
                builder.Build();
        }


    }
}