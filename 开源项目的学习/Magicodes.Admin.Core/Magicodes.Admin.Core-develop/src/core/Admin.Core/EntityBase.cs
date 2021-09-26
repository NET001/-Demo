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

namespace Magicodes.Admin.Core
{
    /// <summary>
    ///     基础模型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityBase<TKey> :
        Entity<TKey>,
        IFullAudited,
        IMayHaveTenant
    {
        /// <summary>
        ///     创建者UserId
        /// </summary>
        [Display(Name = "创建者UserId")]
        public long? CreatorUserId { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///     最后修改者UserId
        /// </summary>
        [Display(Name = "最后修改者UserId")]
        public long? LastModifierUserId { get; set; }

        /// <summary>
        ///     最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        ///     删除者UserId
        /// </summary>
        [Display(Name = "删除者UserId")]
        public long? DeleterUserId { get; set; }

        /// <summary>
        ///     删除时间
        /// </summary>
        [Display(Name = "删除时间")]
        public DateTime? DeletionTime { get; set; }

        /// <summary>
        ///     是否删除
        /// </summary>
        [Display(Name = "是否删除")]
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     租户Id
        /// </summary>
        [Display(Name = "租户Id")]
        public int? TenantId { get; set; }
    }
}