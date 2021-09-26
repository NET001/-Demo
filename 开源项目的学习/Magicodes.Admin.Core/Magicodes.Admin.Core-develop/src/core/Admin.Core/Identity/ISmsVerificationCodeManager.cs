// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ISmsVerificationCodeManager.cs
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
using System.Threading.Tasks;

namespace Magicodes.Admin.Core.Identity
{
    /// <summary>
    ///     短信验证码管理器
    /// </summary>
    public interface ISmsVerificationCodeManager
    {
        /// <summary>
        ///     创建验证码
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="tag">业务标记</param>
        /// <param name="repeatSecs">多少秒内不得重复发送</param>
        /// <param name="expiredTime">过期时间</param>
        /// <returns></returns>
        Task<string> Create(string phoneNumber, string tag = null, int repeatSecs = 60, DateTime? expiredTime = null);

        /// <summary>
        ///     创建并发送短信验证码
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="tag">业务标记</param>
        /// <param name="repeatSecs">多少秒内不得重复发送</param>
        /// <param name="expiredTime">过期时间</param>
        /// <returns></returns>
        Task CreateAndSendVerificationMessage(string phoneNumber, string tag = null, int repeatSecs = 60,
            DateTime? expiredTime = null);

        /// <summary>
        ///     验证
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="code">验证码</param>
        /// <param name="tag">业务标记</param>
        /// <returns></returns>
        Task<bool> VerifyCode(string phoneNumber, string code, string tag = null);

        /// <summary>
        ///     验证验证码并且提示友好错误信息
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="code">验证码</param>
        /// <param name="tag">业务标记</param>
        /// <returns></returns>
        Task VerifyCodeAndShowUserFriendlyException(string phoneNumber, string code, string tag = null);
    }
}