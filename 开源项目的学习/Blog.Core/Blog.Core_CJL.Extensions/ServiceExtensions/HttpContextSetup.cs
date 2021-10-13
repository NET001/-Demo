using Blog.Core_CJL.Common.HttpContextUser;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Extensions.ServiceExtensions
{
    /// <summary>
    /// HttpContext 相关服务
    /// </summary>
    public static class HttpContextSetup
    {
        public static void AddHttpContextSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //单例模式的依赖注入
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //会话级别的依赖注入
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
