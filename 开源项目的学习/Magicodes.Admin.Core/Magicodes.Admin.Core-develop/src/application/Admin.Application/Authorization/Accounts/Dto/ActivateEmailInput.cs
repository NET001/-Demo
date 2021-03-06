// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ActivateEmailInput.cs
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
using System.Web;
using Abp.Runtime.Security;
using Abp.Runtime.Validation;

namespace Magicodes.Admin.Application.Authorization.Accounts.Dto
{
    public class ActivateEmailInput : IShouldNormalize
    {
        public long UserId { get; set; }

        public string ConfirmationCode { get; set; }

        /// <summary>
        ///     Encrypted values for {TenantId}, {UserId} and {ConfirmationCode}
        /// </summary>
        public string c { get; set; }

        public void Normalize()
        {
            ResolveParameters();
        }

        protected virtual void ResolveParameters()
        {
            if (!string.IsNullOrEmpty(c))
            {
                var parameters = SimpleStringCipher.Instance.Decrypt(c);
                var query = HttpUtility.ParseQueryString(parameters);

                if (query["userId"] != null) UserId = Convert.ToInt32(query["userId"]);

                if (query["confirmationCode"] != null) ConfirmationCode = query["confirmationCode"];
            }
        }
    }
}