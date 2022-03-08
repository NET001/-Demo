using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Principal;
using System.Security.Claims;

namespace Attestation
{
    internal class Program
    {
        private static Dictionary<string, string> _accounts;

        static Program()
        {
            _accounts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _accounts.Add("Foo", "password");
            _accounts.Add("Bar", "password");
            _accounts.Add("Baz", "password");
        }
        static void Main(string[] args)
        {
            Demo1();
        }

        //一个简单的认证案例
        static void Demo1()
        {
            Host.CreateDefaultBuilder()
             .ConfigureWebHostDefaults(builder => builder
                 .ConfigureServices(svcs => svcs
                     .AddRouting()
                     //设置为Cookie方案
                     .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme).AddCookie())
                 .Configure(app => app
                     .UseAuthentication()
                     .UseRouting()
                     .UseEndpoints(endpoints =>
                     {
                         endpoints.Map(pattern: "/", RenderHomePageAsync);
                         endpoints.Map("Account/Login", SignInAsync);
                         endpoints.Map("Account/Logout", SignOutAsync);
                     })))
             .Build()
             .Run();
        }
        public static async Task RenderHomePageAsync(HttpContext context)
        {
            //验证是否认证了
            if (context?.User?.Identity?.IsAuthenticated == true)
            {
                await context.Response.WriteAsync(
                    @"<html>
                    <head><title>Index</title></head>
                    <body>" +
                            $"<h3>Welcome {context.User.Identity.Name}</h3>" +
                            @"<a href='Account/Logout'>Sign Out</a>
                    </body>
                </html>");
            }
            else
            {
                //默认跳转到Account/Login
                await context.ChallengeAsync();
            }
        }
        public static async Task SignInAsync(HttpContext context)
        {
            //跳转到登录页面
            if (string.Compare(context.Request.Method, "GET") == 0)
            {
                await RenderLoginPageAsync(context, null, null, null);
            }
            //进行登录验证
            else
            {
                var userName = context.Request.Form["username"];
                var password = context.Request.Form["password"];
                if (_accounts.TryGetValue(userName, out var pwd) && pwd == password)
                {
                    //密码验证成功,设置一个身份
                    var identity = new GenericIdentity(userName, "Passord");
                    var principal = new ClaimsPrincipal(identity);
                    //登录成功
                    await context.SignInAsync(principal);
                }
                else
                {
                    //登录失败
                    await RenderLoginPageAsync(context, userName, password, "Invalid user name or password!");
                }
            }
        }
        private static Task RenderLoginPageAsync(HttpContext context, string userName, string password, string errorMessage)
        {
            context.Response.ContentType = "text/html";
            return context.Response.WriteAsync(
                @"<html>
                <head><title>Login</title></head>
                <body>
                    <form method='post'>" +
                        $"<input type='text' name='username' placeholder='User name' value = '{userName}' /> " +
                        $"<input type='password' name='password' placeholder='Password' value = '{password}' /> " +
                       @"<input type='submit' value='Sign In' />
                    </form>" +
                        $"<p style='color:red'>{errorMessage}</p>" +
                    @"</body>
            </html>");
        }


        public static async Task SignOutAsync(HttpContext context)
        {
            await context.SignOutAsync();
            context.Response.Redirect("/");
        }
    }
}
