// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ChatAppService.cs
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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.RealTime;
using Abp.Runtime.Session;
using Abp.Timing;
using Magicodes.Admin.Application.Chat.Dto;
using Magicodes.Admin.Application.Core;
using Magicodes.Admin.Application.Friendships.Dto;
using Magicodes.Admin.Core.Chat;
using Magicodes.Admin.Core.Friendships.Cache;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.Application.Chat
{
    public class ChatAppService : AdminAppServiceBase, IChatAppService
    {
        private readonly IChatCommunicator _chatCommunicator;
        private readonly IRepository<ChatMessage, long> _chatMessageRepository;
        private readonly IOnlineClientManager<ChatChannel> _onlineClientManager;
        private readonly IUserFriendsCache _userFriendsCache;

        public ChatAppService(
            IRepository<ChatMessage, long> chatMessageRepository,
            IUserFriendsCache userFriendsCache,
            IOnlineClientManager<ChatChannel> onlineClientManager,
            IChatCommunicator chatCommunicator)
        {
            _chatMessageRepository = chatMessageRepository;
            _userFriendsCache = userFriendsCache;
            _onlineClientManager = onlineClientManager;
            _chatCommunicator = chatCommunicator;
        }

        [DisableAuditing]
        public GetUserChatFriendsWithSettingsOutput GetUserChatFriendsWithSettings()
        {
            var userIdentifier = AbpSession.ToUserIdentifier();
            if (userIdentifier == null) return new GetUserChatFriendsWithSettingsOutput();

            var cacheItem = _userFriendsCache.GetCacheItem(userIdentifier);
            var friends = ObjectMapper.Map<List<FriendDto>>(cacheItem.Friends);

            foreach (var friend in friends)
                friend.IsOnline = _onlineClientManager.IsOnline(
                    new UserIdentifier(friend.FriendTenantId, friend.FriendUserId)
                );

            return new GetUserChatFriendsWithSettingsOutput
            {
                Friends = friends,
                ServerTime = Clock.Now
            };
        }

        [DisableAuditing]
        public async Task<ListResultDto<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input)
        {
            var userId = AbpSession.GetUserId();
            var messages = await _chatMessageRepository.GetAll()
                .WhereIf(input.MinMessageId.HasValue, m => m.Id < input.MinMessageId.Value)
                .Where(m => m.UserId == userId && m.TargetTenantId == input.TenantId && m.TargetUserId == input.UserId)
                .OrderByDescending(m => m.CreationTime)
                .Take(50)
                .ToListAsync();

            messages.Reverse();

            return new ListResultDto<ChatMessageDto>(ObjectMapper.Map<List<ChatMessageDto>>(messages));
        }

        public async Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input)
        {
            var userId = AbpSession.GetUserId();
            var tenantId = AbpSession.TenantId;

            // receiver messages
            var messages = await _chatMessageRepository
                .GetAll()
                .Where(m =>
                    m.UserId == userId &&
                    m.TargetTenantId == input.TenantId &&
                    m.TargetUserId == input.UserId &&
                    m.ReadState == ChatMessageReadState.Unread)
                .ToListAsync();

            if (!messages.Any()) return;

            foreach (var message in messages) message.ChangeReadState(ChatMessageReadState.Read);

            // sender messages
            using (CurrentUnitOfWork.SetTenantId(input.TenantId))
            {
                var reverseMessages = await _chatMessageRepository.GetAll()
                    .Where(m => m.UserId == input.UserId && m.TargetTenantId == tenantId && m.TargetUserId == userId)
                    .ToListAsync();

                if (!reverseMessages.Any()) return;

                foreach (var message in reverseMessages) message.ChangeReceiverReadState(ChatMessageReadState.Read);
            }

            var userIdentifier = AbpSession.ToUserIdentifier();
            var friendIdentifier = input.ToUserIdentifier();

            _userFriendsCache.ResetUnreadMessageCount(userIdentifier, friendIdentifier);

            var onlineUserClients = _onlineClientManager.GetAllByUserId(userIdentifier);
            if (onlineUserClients.Any())
                await _chatCommunicator.SendAllUnreadMessagesOfUserReadToClients(onlineUserClients, friendIdentifier);

            var onlineFriendClients = _onlineClientManager.GetAllByUserId(friendIdentifier);
            if (onlineFriendClients.Any())
                await _chatCommunicator.SendReadStateChangeToClients(onlineFriendClients, userIdentifier);
        }
    }
}