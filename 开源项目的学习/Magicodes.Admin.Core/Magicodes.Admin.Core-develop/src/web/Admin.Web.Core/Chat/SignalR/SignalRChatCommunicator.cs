// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : SignalRChatCommunicator.cs
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
using System.Threading.Tasks;
using Abp;
using Abp.Dependency;
using Abp.ObjectMapping;
using Abp.RealTime;
using Castle.Core.Logging;
using Magicodes.Admin.Application.Chat.Dto;
using Magicodes.Admin.Application.Friendships.Dto;
using Magicodes.Admin.Core.Chat;
using Magicodes.Admin.Core.Friendships;
using Microsoft.AspNetCore.SignalR;

namespace Magicodes.Admin.Web.Core.Chat.SignalR
{
    public class SignalRChatCommunicator : IChatCommunicator, ITransientDependency
    {
        private readonly IHubContext<ChatHub> _chatHub;

        private readonly IObjectMapper _objectMapper;

        public SignalRChatCommunicator(
            IObjectMapper objectMapper,
            IHubContext<ChatHub> chatHub)
        {
            _objectMapper = objectMapper;
            _chatHub = chatHub;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        ///     Reference to the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        public async Task SendMessageToClient(IReadOnlyList<IOnlineClient> clients, ChatMessage message)
        {
            foreach (var client in clients)
            {
                var signalRClient = GetSignalRClientOrNull(client);
                if (signalRClient == null) return;

                await signalRClient.SendAsync("getChatMessage", _objectMapper.Map<ChatMessageDto>(message));
            }
        }

        public async Task SendFriendshipRequestToClient(IReadOnlyList<IOnlineClient> clients, Friendship friendship,
            bool isOwnRequest, bool isFriendOnline)
        {
            foreach (var client in clients)
            {
                var signalRClient = GetSignalRClientOrNull(client);
                if (signalRClient == null) return;

                var friendshipRequest = _objectMapper.Map<FriendDto>(friendship);
                friendshipRequest.IsOnline = isFriendOnline;

                await signalRClient.SendAsync("getFriendshipRequest", friendshipRequest, isOwnRequest);
            }
        }

        public async Task SendUserConnectionChangeToClients(IReadOnlyList<IOnlineClient> clients, UserIdentifier user,
            bool isConnected)
        {
            foreach (var client in clients)
            {
                var signalRClient = GetSignalRClientOrNull(client);
                if (signalRClient == null) continue;

                await signalRClient.SendAsync("getUserConnectNotification", user, isConnected);
            }
        }

        public async Task SendUserStateChangeToClients(IReadOnlyList<IOnlineClient> clients, UserIdentifier user,
            FriendshipState newState)
        {
            foreach (var client in clients)
            {
                var signalRClient = GetSignalRClientOrNull(client);
                if (signalRClient == null) continue;

                await signalRClient.SendAsync("getUserStateChange", user, newState);
            }
        }

        public async Task SendAllUnreadMessagesOfUserReadToClients(IReadOnlyList<IOnlineClient> clients,
            UserIdentifier user)
        {
            foreach (var client in clients)
            {
                var signalRClient = GetSignalRClientOrNull(client);
                if (signalRClient == null) continue;

                await signalRClient.SendAsync("getallUnreadMessagesOfUserRead", user);
            }
        }

        public async Task SendReadStateChangeToClients(IReadOnlyList<IOnlineClient> clients, UserIdentifier user)
        {
            foreach (var client in clients)
            {
                var signalRClient = GetSignalRClientOrNull(client);
                if (signalRClient == null) continue;

                await signalRClient.SendAsync("getReadStateChange", user);
            }
        }

        private IClientProxy GetSignalRClientOrNull(IOnlineClient client)
        {
            var signalRClient = _chatHub.Clients.Client(client.ConnectionId);
            if (signalRClient == null)
            {
                Logger.Debug("Can not get chat user " + client.UserId + " from SignalR hub!");
                return null;
            }

            return signalRClient;
        }
    }
}