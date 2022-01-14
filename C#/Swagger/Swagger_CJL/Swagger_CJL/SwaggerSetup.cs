using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Swagger_CJL
{

    /// <summary>
    /// Swagger 启动服务
    /// </summary>
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var ApiName = "CJL";

            //swagger文档配置
            services.AddSwaggerGen(c =>
            {
                string version = "v1";
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
                // 开启加权小锁固定写法
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();


            });
        }
    }
}
