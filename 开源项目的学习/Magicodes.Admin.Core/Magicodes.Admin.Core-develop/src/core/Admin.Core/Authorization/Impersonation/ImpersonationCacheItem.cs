// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ImpersonationCacheItem.cs
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

namespace Magicodes.Admin.Core.Authorization.Impersonation
{
    [Serializable]
    public class ImpersonationCacheItem
    {
        public const string CacheName = "AppImpersonationCache";

        public ImpersonationCacheItem()
        {
        }

        public ImpersonationCacheItem(int? targetTenantId, long targetUserId, bool isBackToImpersonator)
        {
            TargetTenantId = targetTenantId;
            TargetUserId = targetUserId;
            IsBackToImpersonator = isBackToImpersonator;
        }

        public int? ImpersonatorTenantId { get; set; }

        public long ImpersonatorUserId { get; set; }

        public int? TargetTenantId { get; set; }

        public long TargetUserId { get; set; }

        public bool IsBackToImpersonator { get; set; }
    }
}