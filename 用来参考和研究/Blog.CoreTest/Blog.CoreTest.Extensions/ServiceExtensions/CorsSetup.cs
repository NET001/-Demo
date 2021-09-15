using Blog.CoreTest.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Extensions.ServiceExtensions
{
    public static class CorsSetup
    {
        public static void AddCorsSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //设置跨域问题
            services.AddCors(c =>
            {
                //判断是否不是开启所有域名的跨域
                if (!Appsettings.app(new string[] { "Startup", "Cors", "EnableAllIPs" }).ObjToBool())
                {
                    //添加跨域策略(策略名称,策略配置)
                    c.AddPolicy(Appsettings.app(new string[] { "Startup", "Cors", "PolicyName" }),
                        //大部分对外暴露的各种配置都是通过委托来进行实现的
                        policy =>
                        {
                            policy
                            //前端域名请求集合
                            .WithOrigins(Appsettings.app(new string[] { "Startup", "Cors", "IPs" }).Split(','))
                            //允许策略访问头
                            .AllowAnyHeader()//Ensures that the policy allows any header.
                            //允许策略访问方法
                            .AllowAnyMethod();
                        });
                }
                else
                {
                    //允许任意跨域请求
                    c.AddPolicy(Appsettings.app(new string[] { "Startup", "Cors", "PolicyName" }),
                        policy =>
                        {
                            policy
                            .SetIsOriginAllowed((host) => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                        });
                }

            });
        }
    }
}
