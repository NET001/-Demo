// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : SmsVerificationCodeCacheItem.cs
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
using Abp.Timing;

namespace Magicodes.Admin.Core.Identity.Cache
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class SmsVerificationCodeCacheItem
    {
        public const string CacheName = "AppSmsVerificationCodeCache";

        /// <summary>
        /// </summary>
        public SmsVerificationCodeCacheItem()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="code"></param>
        public SmsVerificationCodeCacheItem(string code)
        {
            Code = code;
        }

        /// <summary>
        ///     验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = Clock.Now;

        /// <summary>
        ///     过期时间
        /// </summary>
        public DateTime? ExpiredTime { get; set; }
    }
}