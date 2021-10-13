using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT_CJL
{

    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            var symmetricKeyAsBase64 = "sdfsdfsrty45634kkhllghtdgdfss345t678fs";
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var Issuer = "Issuer";
            var Audience = "Audience";
            //对秘钥进行加密
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var permissionRequirement = new PermissionRequirement(
                Issuer,//发行人
                Audience,//听众
                signingCredentials,//签名凭据
                TimeSpan.FromSeconds(60 * 60)//接口的过期时间
                );
            //自定义赋值的策略授权
            services.AddAuthorization(options =>
            {
                //添加自定义授权策略
                options.AddPolicy("CJL",
                    policy => policy.Requirements.Add(permissionRequirement));
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //添加单个实例全局
            services.AddSingleton(permissionRequirement);
        }
    }
}
