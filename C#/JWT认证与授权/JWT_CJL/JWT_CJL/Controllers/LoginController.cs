
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT_CJL.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("Index")]
        [Authorize]
        public string Index()
        {
            return "11";
        }
        /// <summary>
        /// 获取JWT的方法3：整个系统主要方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("JWTToken3.0")]
        [AllowAnonymous]
        public async Task<string> GetJwtToken3(string name = "", string pass = "")
        {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(JwtRegisteredClaimNames.Jti,"admin"),
                   };

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

            var token = JwtToken.BuildJwtToken(claims.ToArray(), permissionRequirement);
            return token;
        }
    }
}