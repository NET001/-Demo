// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : EntityBase.cs
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
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Magicodes.Admin.Core.Custom
{
    /// <summary>
    ///     基础模型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class CustomEntityBase<TKey> :
        Entity<TKey>,
        IMayHaveTenant
    {
        /// <summary>
        ///     租户Id
        /// </summary>
        [Display(Name = "租户Id")]
        public int? TenantId { get; set; }
    }
}