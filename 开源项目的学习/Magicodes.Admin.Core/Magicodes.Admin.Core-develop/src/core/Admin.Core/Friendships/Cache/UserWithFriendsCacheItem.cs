// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UserWithFriendsCacheItem.cs
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
using System.Collections.Generic;

namespace Magicodes.Admin.Core.Friendships.Cache
{
    public class UserWithFriendsCacheItem
    {
        public int? TenantId { get; set; }

        public long UserId { get; set; }

        public string TenancyName { get; set; }

        public string UserName { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public List<FriendCacheItem> Friends { get; set; }
    }
}