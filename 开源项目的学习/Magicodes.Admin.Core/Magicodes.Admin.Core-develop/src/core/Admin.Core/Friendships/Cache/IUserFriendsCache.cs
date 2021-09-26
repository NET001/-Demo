// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : IUserFriendsCache.cs
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

using Abp;

namespace Magicodes.Admin.Core.Friendships.Cache
{
    public interface IUserFriendsCache
    {
        UserWithFriendsCacheItem GetCacheItem(UserIdentifier userIdentifier);

        UserWithFriendsCacheItem GetCacheItemOrNull(UserIdentifier userIdentifier);

        void ResetUnreadMessageCount(UserIdentifier userIdentifier, UserIdentifier friendIdentifier);

        void IncreaseUnreadMessageCount(UserIdentifier userIdentifier, UserIdentifier friendIdentifier, int change);

        void AddFriend(UserIdentifier userIdentifier, FriendCacheItem friend);

        void RemoveFriend(UserIdentifier userIdentifier, FriendCacheItem friend);

        void UpdateFriend(UserIdentifier userIdentifier, FriendCacheItem friend);
    }
}