using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Blog.Core_CJL.Common;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Blog.Core_CJL.Extensions.Authorizations.Policys;
using Blog.Core_CJL.Common.Helper;
using Blog.Core_CJL.Common.DB;

namespace Blog.Core_CJL.Extensions
{
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            //若为异常则报错
            if (services == null) throw new ArgumentNullException(nameof(services));
            //添加接口授权策略
            //获得秘钥的信息
            var symmetricKeyAsBase64 = AppSecretConfig.Audience_Secret_String;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            //获得一个对称秘钥的实例
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var Issuer = Appsettings.app(new string[] { "AUdience", "Issuer" });
            var Audience = Appsettings.app(new string[] { "Audience", "Audience" });
            //对秘钥进行加密
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            //进行动态赋值
            var permission = new List<PermissionItem>();


            var permissionRequirement = new PermissionRequirement(
                "api/denied",//拒绝授权的跳转地址
                permission,
                ClaimTypes.Role,//基于角色的授权
                Issuer,//发行人
                Audience,//听众
                signingCredentials,//签名凭据
                TimeSpan.FromSeconds(60 * 60)//接口的过期时间
                );
            //自定义赋值的策略授权
            services.AddAuthorization(options =>
            {
                //添加自定义授权策略
                options.AddPolicy(Permissions.Name,
                    policy => policy.Requirements.Add(permissionRequirement));
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //注入权限处理器
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            //添加单个实例全局
            services.AddSingleton(permissionRequirement);
        }
    }
}
