using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Route
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Demo3();
        }
        /// <summary>
        /// 路由的基本映射
        /// </summary>
        static void Demo1()
        {
            Host.CreateDefaultBuilder()
              .ConfigureWebHostDefaults(builder => builder
                  .ConfigureServices(svcs => svcs.AddRouting())
                  .Configure(app => app
                      .UseRouting()
                      .UseEndpoints(endpoints => endpoints.MapGet("weather/{city}/{days}", WeatherForecast))))
              .Build()
              .Run();
        }
        /// <summary>
        /// 设置路由的内联约束
        /// </summary>
        static void Demo2()
        {
            var template = @"weather/{city:regex(^0\d{{2,3}}$)}/{days:int:range(1,4)}";
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddRouting())
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(routes => routes.MapGet(template, WeatherForecast))))
                .Build()
                .Run();
        }
        /// <summary>
        /// 定义默认路由参数
        /// </summary>
        static void Demo3()
        {
            var template = "weather/{city?}/{days?}";
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddRouting())
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(routes => routes.MapGet(template, WeatherForecast2))))
                .Build()
                .Run();
        }
        /// <summary>
        /// 自带默认值的路由
        /// </summary>
        static void Demo4()
        {
            var template = "weather/{city=010}/{days=4}";
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddRouting())
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(routes => routes.MapGet(template, WeatherForecast2))))
                .Build()
                .Run();
        }
        /// <summary>
        /// 多段路由
        /// </summary>
        static void Demo5()
        {
            var template = "weather/{city}/{year}.{month}.{day}"; Host.CreateDefaultBuilder()
                 .ConfigureWebHostDefaults(builder => builder
                     .ConfigureServices(svcs => svcs.AddRouting())
                     .Configure(app => app
                         .UseRouting()
                         .UseEndpoints(routes => routes.MapGet(template, WeatherForecast3))))
                 .Build()
                 .Run();
        }
        /// <summary>
        /// 路由过滤
        /// </summary>
        static void Demo6()
        {

            var template = "resources/{lang:culture}/{resourceName:required}";
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs
                        .AddRouting(options => options.ConstraintMap
                           .Add("culture", typeof(CultureConstraint))))
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(routes => routes.MapGet(
                            template, BuildHandler(routes.CreateApplicationBuilder())))))
                .Build()
                .Run();
        }

        #region Demo5
        public static async Task WeatherForecast3(HttpContext context)
        {
            var values = context.GetRouteData().Values;
            var city = values["city"].ToString();
            city = _cities[city];
            int year = int.Parse(values["year"].ToString());
            int month = int.Parse(values["month"].ToString());
            int day = int.Parse(values["day"].ToString());
            var report = new WeatherReport(city, new DateTime(year, month, day));
            await RendWeatherAsync(context, report);
        }
        #endregion

        #region Demo3、Demo4
        public static async Task WeatherForecast2(HttpContext context)
        {
            var routeValues = context.GetRouteData().Values;
            var city = routeValues.TryGetValue("city", out var v1)
             ? (string)v1
             : "010";
            city = _cities[city];
            var days = routeValues.TryGetValue("days", out var v2)
                ? int.Parse(v2.ToString())
                : 4;

            var report = new WeatherReport(city, days);
            await RendWeatherAsync(context, report);
        }
        #endregion

        #region Demo1、Demo2、Demo3、Demo4
        private static Dictionary<string, string> _cities = new Dictionary<string, string>
        {
            ["010"] = "北京",
            ["028"] = "成都",
            ["0512"] = "苏州"
        };
        public static async Task WeatherForecast(HttpContext context)
        {
            var city = (string)context.GetRouteData().Values["city"];
            city = _cities[city];
            int days = int.Parse(context.GetRouteData().Values["days"].ToString());
            var report = new WeatherReport(city, days);
            await RendWeatherAsync(context, report);
        }
        private static async Task RendWeatherAsync(HttpContext context, WeatherReport report)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync("<html><head><title>Weather</title></head><body>");
            await context.Response.WriteAsync($"<h3>{report.City}</h3>");
            foreach (var it in report.WeatherInfos)
            {
                await context.Response.WriteAsync($"{it.Key.ToString("yyyy-MM-dd")}:");
                await context.Response.WriteAsync($"{it.Value.Condition}({ it.Value.LowTemperature}℃ ~ { it.Value.HighTemperature}℃)<br/><br/> ");
            }
            await context.Response.WriteAsync("</body></html>");
        }

        private class WeatherReport
        {
            private static string[] _conditions = new string[] { "晴", "多云", "小雨" };
            private static Random _random = new Random();

            public string City { get; }
            public IDictionary<DateTime, WeatherInfo> WeatherInfos { get; }

            public WeatherReport(string city, int days)
            {
                City = city;
                WeatherInfos = new Dictionary<DateTime, WeatherInfo>();
                for (int i = 0; i < days; i++)
                {
                    this.WeatherInfos[DateTime.Today.AddDays(i + 1)] = new WeatherInfo
                    {
                        Condition = _conditions[_random.Next(0, 2)],
                        HighTemperature = _random.Next(20, 30),
                        LowTemperature = _random.Next(10, 20)
                    };
                }
            }

            public WeatherReport(string city, DateTime date)
            {
                City = city;
                WeatherInfos = new Dictionary<DateTime, WeatherInfo>
                {
                    [date] = new WeatherInfo
                    {
                        Condition = _conditions[_random.Next(0, 2)],
                        HighTemperature = _random.Next(20, 30),
                        LowTemperature = _random.Next(10, 20)
                    }
                };
            }

            public class WeatherInfo
            {
                public string Condition { get; set; }
                public double HighTemperature { get; set; }
                public double LowTemperature { get; set; }
            }
        }

        #endregion

        #region Demo6

        static RequestDelegate BuildHandler(IApplicationBuilder app)
        {
            app.UseMiddleware<LocalizationMiddleware>("lang")
                .Run(async context =>
                {
                    var values = context.GetRouteData().Values;
                    var resourceName = values["resourceName"].ToString().ToLower();
                    await context.Response.WriteAsync(
                    Resources.ResourceManager.GetString(resourceName));
                });
            return app.Build();
        }

        public class CultureConstraint : IRouteConstraint
        {
            public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
            {
                try
                {
                    if (values.TryGetValue(routeKey, out object value))
                    {
                        return !new CultureInfo(value.ToString()).EnglishName.StartsWith("Unknown Language");
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public class LocalizationMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly string _routeKey;

            public LocalizationMiddleware(RequestDelegate next, string routeKey)
            {
                _next = next;
                _routeKey = routeKey;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                var currentCulture = CultureInfo.CurrentCulture;
                var currentUICulture = CultureInfo.CurrentUICulture;
                try
                {
                    if (context.GetRouteData().Values.TryGetValue(_routeKey, out var culture))
                    {
                        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(culture.ToString());
                    }
                    await _next(context);
                }
                finally
                {
                    CultureInfo.CurrentCulture = currentCulture;
                    CultureInfo.CurrentUICulture = currentUICulture;
                }
            }
        }
        #endregion
    }
}
