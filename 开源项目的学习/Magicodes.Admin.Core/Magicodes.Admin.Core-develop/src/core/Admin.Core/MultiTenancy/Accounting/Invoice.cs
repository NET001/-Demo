// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : Invoice.cs
//           description :
// 
//           created by 雪雁 at  2019-06-17 10:17
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Magicodes.Admin.Core.MultiTenancy.Accounting
{
    [Table("AppInvoices")]
    public class Invoice : Entity<int>
    {
        public string InvoiceNo { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string TenantLegalName { get; set; }

        public string TenantAddress { get; set; }

        public string TenantTaxNo { get; set; }

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