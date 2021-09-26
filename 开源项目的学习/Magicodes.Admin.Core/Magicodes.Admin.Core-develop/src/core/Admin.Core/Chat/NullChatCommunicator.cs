// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : NullChatCommunicator.cs
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

using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Abp.RealTime;
using Magicodes.Admin.Core.Friendships;

namespace Magicodes.Admin.Core.Chat
{
    public class NullChatCommunicator : IChatCommunicator
    {
        public async Task SendMessageToClient(IReadOnlyList<IOnlineClient> clients, ChatMessage message)
        {
            await Task.CompletedTask;
        }

        public async Task SendFriendshipRequestToClient(IReadOnlyList<IOnlineClient> clients, Friendship friend,
            bool isOwnRequest, bool isFriendOnline)
        {
            await Task.CompletedTask;
        }

        public async Task SendUserConnectionChangeToClients(IReadOnlyList<IOnlineClient> clients, UserIdentifier user,
            bool isConnected)
        {
            await Task.CompletedTask;
        }

        public async Task SendUserStateChangeToClients(IReadOnlyList<IOnlineClient> clients, UserIdentifier user,
            FriendshipState newState)
        {
            await Task.CompletedTask;
        }

        public async Task SendAllUnreadMessagesOfUserReadToClients(IReadOnlyList<IOnlineClient> clients,
            UserIdentifier user)
        {
            await Task.CompletedTask;
        }

        public async Task SendReadStateChangeToClients(IReadOnlyList<IOnlineClient> clients, UserIdentifier user)
        {
            await Task.CompletedTask;
        }
    }
}