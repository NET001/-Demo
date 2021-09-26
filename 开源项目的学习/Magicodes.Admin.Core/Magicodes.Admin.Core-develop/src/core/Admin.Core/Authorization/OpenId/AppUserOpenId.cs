// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppUserOpenId.cs
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
using Magicodes.Admin.Core.Authorization.Users;

namespace Magicodes.Admin.Core.Authorization.OpenId
{
    /// <summary>
    ///     用户OpenId关联表
    /// </summary>
    public class AppUserOpenId : Entity<long>, IMayHaveTenant, IHasCreationTime, IHasModificationTime
    {
        /// <summary>
        ///     开放平台用户唯一标识
        /// </summary>
        [MaxLength(50)]
        public string OpenId { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     用户信息
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///     来自（平台）
        /// </summary>
        public OpenIdPlatforms From { get; set; }

        /// <summary>
        ///     开放平台统一Id
        /// </summary>
        [MaxLength(50)]
        public string UnionId { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///     最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        ///     租户Id
        /// </summary>
        public int? TenantId { get; set; }
    }
}