// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TenantBillingSettingsEditDto.cs
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

namespace Magicodes.Admin.Application.Configuration.Tenants.Dto
{
    public class TenantBillingSettingsEditDto
    {
        /// <summary>
        ///     抬头名称
        /// </summary>
        public string LegalName { get; set; }

        /// <summary>
        ///     地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     税号
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        ///     联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        ///     银行账户
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        ///     开户行
        /// </summary>
        public string Bank { get; set; }
    }
}