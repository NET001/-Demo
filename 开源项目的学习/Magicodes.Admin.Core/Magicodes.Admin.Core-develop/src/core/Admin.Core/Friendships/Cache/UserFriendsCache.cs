// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UserFriendsCache.cs
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

using System.Linq;
using Abp;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using Abp.Threading;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.Chat;

namespace Magicodes.Admin.Core.Friendships.Cache
{
    public class UserFriendsCache : IUserFriendsCache, ISingletonDependency
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<ChatMessage, long> _chatMessageRepository;
        private readonly IRepository<Friendship, long> _friendshipRepository;

        private readonly object _syncObj = new object();
        private readonly ITenantCache _tenantCache;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserManager _userManager;

        public UserFriendsCache(
            ICacheManager cacheManager,
            IRepository<Friendship, long> friendshipRepository,
            IRepository<ChatMessage, long> chatMessageRepository,
            ITenantCache tenantCache,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _cacheManager = cacheManager;
            _friendshipRepository = friendshipRepository;
            _chatMessageRepository = chatMessageRepository;
            _tenantCache = tenantCache;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public virtual UserWithFriendsCacheItem GetCacheItem(UserIdentifier userIdentifier)
        {
            return _cacheManager
                .GetCache(FriendCacheItem.CacheName)
                .Get<string, UserWithFriendsCacheItem>(userIdentifier.ToUserIdentifierString(),
                    f => GetUserFriendsCacheItemInternal(userIdentifier));
        }

        public virtual UserWithFriendsCacheItem GetCacheItemOrNull(UserIdentifier userIdentifier)
        {
            return _cacheManager
                .GetCache(FriendCacheItem.CacheName)
                .GetOrDefault<string, UserWithFriendsCacheItem>(userIdentifier.ToUserIdentifierString());
        }

        [UnitOfWork]
        public virtual void ResetUnreadMessageCount(UserIdentifier userIdentifier, UserIdentifier friendIdentifier)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null) return;

            lock (_syncObj)
            {
                var friend = user.Friends.FirstOrDefault(
                    f => f.FriendUserId == friendIdentifier.UserId &&
                         f.FriendTenantId == friendIdentifier.TenantId
                );

                if (friend == null) return;

                friend.UnreadMessageCount = 0;
                UpdateUserOnCache(userIdentifier, user);
            }
        }

        [UnitOfWork]
        public virtual void IncreaseUnreadMessageCount(UserIdentifier userIdentifier, UserIdentifier friendIdentifier,
            int change)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null) return;

            lock (_syncObj)
            {
                var friend = user.Friends.FirstOrDefault(
                    f => f.FriendUserId == friendIdentifier.UserId &&
                         f.FriendTenantId == friendIdentifier.TenantId
                );

                if (friend == null) return;

                friend.UnreadMessageCount += change;
                UpdateUserOnCache(userIdentifier, user);
            }
        }

        public void AddFriend(UserIdentifier userIdentifier, FriendCacheItem friend)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null) return;

            lock (_syncObj)
            {
                if (!user.Friends.ContainsFriend(friend))
                {
                    user.Friends.Add(friend);
                    UpdateUserOnCache(userIdentifier, user);
                }
            }
        }

        public void RemoveFriend(UserIdentifier userIdentifier, FriendCacheItem friend)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null) return;

            lock (_syncObj)
            {
                if (user.Friends.ContainsFriend(friend))
                {
                    user.Friends.Remove(friend);
                    UpdateUserOnCache(userIdentifier, user);
                }
            }
        }

        public void UpdateFriend(UserIdentifier userIdentifier, FriendCacheItem friend)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null) return;

            lock (_syncObj)
            {
                var existingFriendIndex = user.Friends.FindIndex(
                    f => f.FriendUserId == friend.FriendUserId &&
                         f.FriendTenantId == friend.FriendTenantId
                );

                if (existingFriendIndex >= 0)
                {
                    user.Friends[existingFriendIndex] = friend;
                    UpdateUserOnCache(userIdentifier, user);
                }
            }
        }

        [UnitOfWork]
        protected virtual UserWithFriendsCacheItem GetUserFriendsCacheItemInternal(UserIdentifier userIdentifier)
        {
            var tenancyName = userIdentifier.TenantId.HasValue
                ? _tenantCache.GetOrNull(userIdentifier.TenantId.Value)?.TenancyName
                : null;

            using (_unitOfWorkManager.Current.SetTenantId(userIdentifier.TenantId))
            {
                var friendCacheItems =
                    (from friendship in _friendshipRepository.GetAll()
                        join chatMessage in _chatMessageRepository.GetAll() on
                            new
                            {
                                userIdentifier.UserId, userIdentifier.TenantId, TargetUserId = friendship.FriendUserId,
                                TargetTenantId = friendship.FriendTenantId, ChatSide = ChatSide.Receiver
                            } equals
                            new
                            {
                                chatMessage.UserId, chatMessage.TenantId, chatMessage.TargetUserId,
                                chatMessage.TargetTenantId, ChatSide = chatMessage.Side
                            } into chatMessageJoined
                        where friendship.UserId == userIdentifier.UserId
                        select new FriendCacheItem
                        {
                            FriendUserId = friendship.FriendUserId,
                            FriendTenantId = friendship.FriendTenantId,
                            State = friendship.State,
                            FriendUserName = friendship.FriendUserName,
                            FriendTenancyName = friendship.FriendTenancyName,
                            FriendProfilePictureId = friendship.FriendProfilePictureId,
                            UnreadMessageCount =
                                chatMessageJoined.Count(cm => cm.ReadState == ChatMessageReadState.Unread)
                        }).ToList();

                var user = AsyncHelper.RunSync(() => _userManager.FindByIdAsync(userIdentifier.UserId.ToString()));

                return new UserWithFriendsCacheItem
                {
                    TenantId = userIdentifier.TenantId,
                    UserId = userIdentifier.UserId,
                    TenancyName = tenancyName,
                    UserName = user.UserName,
                    ProfilePictureId = user.ProfilePictureId,
                    Friends = friendCacheItems
                };
            }
        }

        private void UpdateUserOnCache(UserIdentifier userIdentifier, UserWithFriendsCacheItem user)
        {
            _cacheManager.GetCache(FriendCacheItem.CacheName).Set(userIdentifier.ToUserIdentifierString(), user);
        }
    }
}