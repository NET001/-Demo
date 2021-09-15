using Blog.CoreTest.Common;
using Blog.CoreTest.Extensions.Authorizations.Policys;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Extensions.ServiceExtensions
{

    public static class Authentication_JWTSetup
    {
        public static void AddAuthentication_JWTSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            var symmetricKeyAsBase64 = AppSecretConfig.Audience_Secret_String;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var Issuer = Appsettings.app(new string[] { "Audience", "Issuer" });
            var Audience = Appsettings.app(new string[] { "Audience", "Audience" });

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                //开启签名
                ValidateIssuerSigningKey = true,
                //配置签名
                IssuerSigningKey = signingKey,
                //开启发行人
                ValidateIssuer = true,
                ValidIssuer = Issuer,//发行人
                //开启订阅人
                ValidateAudience = true,
                ValidAudience = Audience,//订阅人
                //令牌持续时间
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            // 开启Bearer认证
            services.AddAuthentication(o =>
            {
                //认证方式为Banner认证
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                //配置一些认证失败的默认处理方案
                o.DefaultChallengeScheme = nameof(ApiResponseHandler);
                o.DefaultForbidScheme = nameof(ApiResponseHandler);
            })
             // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 //配置jwt的秘钥
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     //认证失败后的处理
                     OnChallenge = context =>
                     {
                         context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                         return Task.CompletedTask;
                     },
                     OnAuthenticationFailed = context =>
                     {
                         var jwtHandler = new JwtSecurityTokenHandler();
                         var token = context.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "");

                         if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
                         {
                             var jwtToken = jwtHandler.ReadJwtToken(token);

                             if (jwtToken.Issuer != Issuer)
                             {
                                 context.Response.Headers.Add("Token-Error-Iss", "issuer is wrong!");
                             }

                             if (jwtToken.Audiences.FirstOrDefault() != Audience)
                             {
                                 context.Response.Headers.Add("Token-Error-Aud", "Audience is wrong!");
                             }
                         }
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             })
             .AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });
        }
    }
}