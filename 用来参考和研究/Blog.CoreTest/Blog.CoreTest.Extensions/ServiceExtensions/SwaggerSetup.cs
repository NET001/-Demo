using Blog.CoreTest.Common;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Blog.CoreTest.Extensions.ServiceExtensions.CustomApiVersion;

namespace Blog.CoreTest.Extensions.ServiceExtensions
{

    /// <summary>
    /// Swagger 启动服务
    /// </summary>
    public static class SwaggerSetup
    {

        private static readonly ILog log =
        LogManager.GetLogger(typeof(SwaggerSetup));

        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //程序根目录
            var basePath = AppContext.BaseDirectory;
            //var basePath2 = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            var ApiName = Appsettings.app(new string[] { "Startup", "ApiName" });


            //swagger文档配置
            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        //版本号
                        Version = version,
                        //运行时版本
                        Title = $"{ApiName} 接口文档——{RuntimeInformation.FrameworkDescription}",
                        //界面上的信息
                        Description = $"{ApiName} HTTP API " + version,
                        //添加界面上显示的信息
                        Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://neters.club") },
                        //添加界面上显示的信息
                        License = new OpenApiLicense { Name = ApiName + " 官方文档", Url = new Uri("http://apk.neters.club/.doc/") }
                    });
                    //显示排序
                    c.OrderActionsBy(o => o.RelativePath);
                });
                try
                {
                    //添加接口注释,这个xml文件是根据接口的三斜杠上的注释进行生产的,生成这个xml的需要在属性中进行配置
                    //这个就是刚刚配置的xml文件名
                    var xmlPath = Path.Combine(basePath, "Blog.CoreTest.Api.xml");
                    //默认的第二个参数是false，这个是controller的注释，记得修改
                    c.IncludeXmlComments(xmlPath, true);

                    //这个就是Model层的xml文件名
                    var xmlModelPath = Path.Combine(basePath, "Blog.CoreTest.Model.xml");
                    c.IncludeXmlComments(xmlModelPath);
                }
                catch (Exception ex)
                {
                    log.Error("Blog.CoreTest.xml和Blog.CoreTest.Model.xml 丢失，请检查并拷贝。\n" + ex.Message);
                }

                // 开启加权小锁固定写法
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //下面是开启swagger加权后的权限请求配置
                // ids4和jwt切换
                // Jwt Bearer 认证，必须是 oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }
    }

    /// <summary>
    /// 自定义版本
    /// </summary>
    public class CustomApiVersion
    {
        /// <summary>
        /// Api接口版本 自定义
        /// </summary>
        public enum ApiVersions
        {
            /// <summary>
            /// V1 版本
            /// </summary>
            V1 = 1,
            /// <summary>
            /// V2 版本
            /// </summary>
            V2 = 2,
        }
    }
}
