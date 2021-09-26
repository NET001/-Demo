// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UserFriendCacheSyncronizer.cs
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
using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.ObjectMapping;
using Magicodes.Admin.Core.Chat;

namespace Magicodes.Admin.Core.Friendships.Cache
{
    public class UserFriendCacheSyncronizer :
        IEventHandler<EntityCreatedEventData<Friendship>>,
        IEventHandler<EntityDeletedEventData<Friendship>>,
        IEventHandler<EntityUpdatedEventData<Friendship>>,
        IEventHandler<EntityCreatedEventData<ChatMessage>>,
        ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IUserFriendsCache _userFriendsCache;

        public UserFriendCacheSyncronizer(
            IUserFriendsCache userFriendsCache,
            IObjectMapper objectMapper)
        {
            _userFriendsCache = userFriendsCache;
            _objectMapper = objectMapper;
        }

        public void HandleEvent(EntityCreatedEventData<ChatMessage> eventData)
        {
            var message = eventData.Entity;
            if (message.ReadState == ChatMessageReadState.Unread)
                _userFriendsCache.IncreaseUnreadMessageCount(
                    new UserIdentifier(message.TenantId, message.UserId),
                    new UserIdentifier(message.TargetTenantId, message.TargetUserId),
                    1
                );
        }

        public void HandleEvent(EntityCreatedEventData<Friendship> eventData)
        {
            _userFriendsCache.AddFriend(
                eventData.Entity.ToUserIdentifier(),
                _objectMapper.Map<FriendCacheItem>(eventData.Entity)
            );
        }

        public void HandleEvent(EntityDeletedEventData<Friendship> eventData)
        {
            _userFriendsCache.RemoveFriend(
                eventData.Entity.ToUserIdentifier(),
                _objectMapper.Map<FriendCacheItem>(eventData.Entity)
            );
        }

        public void HandleEvent(EntityUpdatedEventData<Friendship> eventData)
        {
            var friendCacheItem = _objectMapper.Map<FriendCacheItem>(eventData.Entity);
            _userFriendsCache.UpdateFriend(eventData.Entity.ToUserIdentifier(), friendCacheItem);
        }
    }
}