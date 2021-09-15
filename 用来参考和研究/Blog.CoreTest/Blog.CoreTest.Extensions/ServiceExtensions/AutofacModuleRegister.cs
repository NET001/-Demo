using Autofac;
using Autofac.Extras.DynamicProxy;
using Blog.CoreTest.Common;
using Blog.CoreTest.Extensions.AOP;
using Blog.CoreTest.Repository.BASE;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Extensions.ServiceExtensions
{

    public class AutofacModuleRegister : Autofac.Module
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AutofacModuleRegister));
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();


            #region 带有接口层的服务注入

            var servicesDllFile = Path.Combine(basePath, "Blog.CoreTest.Services.dll");
            var repositoryDllFile = Path.Combine(basePath, "Blog.CoreTest.Repository.dll");

            if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            {
                var msg = "Repository.dll和service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";
                log.Error(msg);
                throw new Exception(msg);
            }
            // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
            var cacheType = new List<Type>();
            if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<BlogRedisCacheAOP>();
                cacheType.Add(typeof(BlogRedisCacheAOP));
            }
            if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<BlogCacheAOP>();
                cacheType.Add(typeof(BlogCacheAOP));
            }
            if (Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<BlogTranAOP>();
                cacheType.Add(typeof(BlogTranAOP));
            }
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//注册仓储
            //这里是通过获取dll对整层进行注入，比builder.RegisterType<t>().As<it>();要方便很多
            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            //在程序集中注册所有类型。
            builder.RegisterAssemblyTypes(assemblysServices)
                      //指定将扫描程序集中的类型注册为提供其实现的所有接口。
                      .AsImplementedInterfaces()
                      //配置组件，以便每个依赖组件或对Resolve()的调用都获得一个新的、唯一的实例(默认)。
                      .InstancePerDependency()
                      //配置组件，以便在容器中注册类型的任何属性都将连接到适当服务的实例。
                      .PropertiesAutowired()
                      //在这个下面配置了AOP
                      //在目标类型上启用接口拦截。拦截器将通过类或接口上的Intercept属性来确定，或者通过InterceptedBy()调用来添加。
                      .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                      .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .InstancePerDependency();
            #endregion
        }
    }
}