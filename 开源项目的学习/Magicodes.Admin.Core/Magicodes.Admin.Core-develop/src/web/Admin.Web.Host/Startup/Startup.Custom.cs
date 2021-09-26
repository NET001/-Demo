// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : Startup.Custom.cs
//           description :
// 
//           created by 雪雁 at  2019-06-14 11:22
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using System;
using Abp.Extensions;
using Abp.Hangfire;
using Hangfire;
using Magicodes.Admin.Core.Authorization;
using Magicodes.Admin.Web.Core.IdentityServer;
using Magicodes.SwaggerUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Magicodes.Admin.Web.Host.Startup
{
    public partial class Startup
    {
        /// <summary>
        ///     配置自定义服务
        /// </summary>
        /// <param name="services"></param>
        partial void ConfigureCustomServices(IServiceCollection services)
        {
            _logger.LogInformation($"IdentityServer:IsEnabled:{_appConfiguration["IdentityServer:IsEnabled"]}");
            //Identity server
            if (bool.Parse(_appConfiguration["IdentityServer:IsEnabled"] ?? "false"))
                IdentityServerRegistrar.Register(services, _appConfiguration);

            //添加自定义API文档生成(支持文档配置)
            services.AddCustomSwaggerGen(_appConfiguration);
            _logger.LogInformation($"Abp:Hangfire:IsEnabled:{_appConfiguration["Abp:Hangfire:IsEnabled"]}");
            //仅在后台服务启用
            if (!_appConfiguration["Abp:Hangfire:IsEnabled"].IsNullOrEmpty() &&
                Convert.ToBoolean(_appConfiguration["Abp:Hangfire:IsEnabled"]))
                //使用Hangfire替代默认的任务调度
                services.AddHangfire(config =>
                {
                    config.UseSqlServerStorage(_appConfiguration.GetConnectionString("Default"));
                });
        }

        partial void CustomConfigure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //仅在后台服务启用
            if (!_appConfiguration["Abp:Hangfire:IsEnabled"].IsNullOrEmpty() &&
                Convert.ToBoolean(_appConfiguration["Abp:Hangfire:IsEnabled"]) &&
                !_appConfiguration["Abp:Hangfire:DashboardEnabled"].IsNullOrEmpty() &&
                Convert.ToBoolean(_appConfiguration["Abp:Hangfire:DashboardEnabled"]))
            {
                //启用Hangfire仪表盘
                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    Authorization = new[]
                        {new AbpHangfireAuthorizationFilter(AppPermissions.Pages_Administration_HangfireDashboard)}
                });
                app.UseHangfireServer();
            }

            app.UseMvc(routes =>
            {
                //替换默认的AbpUserConfiguration/GetAll，优化性能
                routes.MapRoute(
                    name: "AbpUserConfiguration",
                    template: "AbpUserConfiguration/{action}",
                    defaults: new { controller = "Configuration", action = "GetAll" });

                routes.MapRoute(
                    name: "oldAbpUserConfiguration",
                    template: "UserConfiguration/{action}",
                    defaults: new { controller = "AbpUserConfiguration", action = "GetAll" });

                routes.MapRoute(
                    "defaultWithArea",
                    "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");

            });

            //启用自定义API文档(支持文档配置)
            app.UseCustomSwaggerUI(_appConfiguration);
        }
    }
}