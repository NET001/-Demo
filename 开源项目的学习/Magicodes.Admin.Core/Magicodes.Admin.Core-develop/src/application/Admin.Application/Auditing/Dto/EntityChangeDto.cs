// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : EntityChangeDto.cs
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
using Abp.Application.Services.Dto;
using Abp.Events.Bus.Entities;

namespace Magicodes.Admin.Application.Auditing.Dto
{
    public class EntityChangeDto : EntityDto<long>
    {
        public DateTime ChangeTime { get; set; }

        public EntityChangeType ChangeType { get; set; }

        public long EntityChangeSetId { get; set; }

        public string EntityId { get; set; }

        public string EntityTypeFullName { get; set; }

        public int? TenantId { get; set; }

        public object EntityEntry { get; set; }
    }
}