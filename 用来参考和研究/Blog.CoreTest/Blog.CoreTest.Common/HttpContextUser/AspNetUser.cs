using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Common.HttpContextUser
{

    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<AspNetUser> _logger;

        public AspNetUser(IHttpContextAccessor accessor, ILogger<AspNetUser> logger)
        {
            _accessor = accessor;
            _logger = logger;
        }
        //获得用户名称
        public string Name => GetName();
        private string GetName()
        {
            //此处的处理是当接口有给授权特性的时候才会走到具体的中间件中去执行相应的权限验证，在这个时候才会吧授权数据赋值到上下文中，所有只有这样才能获取

            //直接从http上下文中获取,若没有获取到就去直接解析token
            if (IsAuthenticated() && _accessor.HttpContext.User.Identity.Name.IsNotEmptyOrNull())
            {
                return _accessor.HttpContext.User.Identity.Name;
            }
            else
            {
                if (!string.IsNullOrEmpty(GetToken()))
                {
                    var getNameType = Permissions.IsUseIds4 ? "name" : "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
                    return GetUserInfoFromToken(getNameType).FirstOrDefault().ObjToString();
                }
            }

            return "";
        }
        //获得jwt中的id
        public int ID => GetClaimValueByType("jti").FirstOrDefault().ObjToInt();
        //是否已认证
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
        //从token中获取jwt字段
        public string GetToken()
        {
            return _accessor.HttpContext?.Request?.Headers["Authorization"].ObjToString().Replace("Bearer ", "");
        }
        //解析token令牌中的用户数据部分
        public List<string> GetUserInfoFromToken(string ClaimType)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = "";

            token = GetToken();
            // token校验token不为空同时满足jwt格式
            if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
                //获得jwt中申明的值
                return (from item in jwtToken.Claims
                        where item.Type == ClaimType
                        select item.Value).ToList();
            }

            return new List<string>() { };
        }
        //获得http上下文中的claim申明
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
        //获得指定申明
        public List<string> GetClaimValueByType(string ClaimType)
        {
            return (from item in GetClaimsIdentity()
                    where item.Type == ClaimType
                    select item.Value).ToList();

        }
    }
}
