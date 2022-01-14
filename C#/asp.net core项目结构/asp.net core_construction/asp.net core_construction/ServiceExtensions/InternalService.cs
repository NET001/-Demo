using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace IoTCenterWebApi.ServiceExtensions
{
    public static class InternalService
    {

        internal static IServiceCollection AddInternalService(this IServiceCollection services)
        {
            var mvcBuilders = services.AddControllers(options =>
            {
                //添加过滤器
                //options.Filters.Add(typeof());
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });

            //添加分布式缓存
            services.AddDistributedMemoryCache();

            //注册请求上下文管理
            services.AddHttpContextAccessor();

            //注册响应压缩
            services.AddResponseCompression();

            //注册响应缓存
            services.AddResponseCaching();

            //注册SignalR
            services.AddSignalR();

            // HTTP 安全服务
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });


            // HttpClientFactory服务
            services.AddHttpClient("sso", c =>
            {
                c.BaseAddress = new Uri("");
                c.Timeout = TimeSpan.FromSeconds(30);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new System.Net.Http.HttpClientHandler()
                {
                    UseDefaultCredentials = true,
                    ServerCertificateCustomValidationCallback = (a, b, c, d) => true
                };
            });
            services.AddMemoryCache();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            return services;
        }
    }
}
