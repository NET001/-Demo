using IoTCenterWebApi.ServiceExtensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace IoTCenterWebApi
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInternalService();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();


            app.UseStatusCodePages();

            // 压缩响应
            app.UseResponseCompression();

            //添加响应缓存
            app.UseResponseCaching();

            //NWebsec.AspNetCore.Middleware是一个安全库

            app.UseRouting();

            app.UseCors(policy =>
            {
                CorsPolicyBuilder corsPolicyBuilder;

                // app专用环境中运行应用程序，去除跨域访问限制
                var allowOrigins = Configuration.GetSection("AllowOrigins").Get<string[]>();
                if (allowOrigins == null || allowOrigins.Length == 0 || (allowOrigins.Length == 1 && allowOrigins[0] == "*"))
                {
                    corsPolicyBuilder = policy.SetIsOriginAllowed(origin => true);
                }
                else
                {
                    corsPolicyBuilder = policy.WithOrigins(allowOrigins)
                       .SetPreflightMaxAge(TimeSpan.FromHours(1))
                       .SetIsOriginAllowedToAllowWildcardSubdomains();
                }

                corsPolicyBuilder.AllowCredentials()
                    .AllowAnyHeader().WithMethods("PUT", "DELETE", "GET", "POST", "OPTIONS");
            });

            //认证中间件
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
