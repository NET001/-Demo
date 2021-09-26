// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ChatUserCollectedDataProvider.cs
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
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Magicodes.Admin.Application.Chat.Dto;
using Magicodes.Admin.Application.Chat.Exporting;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Core.Chat;
using Magicodes.Admin.Core.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Magicodes.Admin.EntityFrameworkCore;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework;

namespace Magicodes.Admin.Application.Gdpr
{
    public class ChatUserCollectedDataProvider : IUserCollectedDataProvider, ITransientDependency
    {
        private readonly IChatMessageListExcelExporter _chatMessageListExcelExporter;
        private readonly IRepository<ChatMessage, long> _chatMessageRepository;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<UserAccount, long> _userAccountRepository;

        public ChatUserCollectedDataProvider(
            IRepository<ChatMessage, long> chatMessageRepository,
            IChatMessageListExcelExporter chatMessageListExcelExporter,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserAccount, long> userAccountRepository,
            IRepository<Tenant> tenantRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _chatMessageListExcelExporter = chatMessageListExcelExporter;
            _unitOfWorkManager = unitOfWorkManager;
            _userAccountRepository = userAccountRepository;
            _tenantRepository = tenantRepository;
        }

        public async Task<List<FileDto>> GetFiles(UserIdentifier user)
        {
            var conversations = await GetUserChatMessages(user.TenantId, user.UserId);

            Dictionary<UserIdentifier, string> relatedUsernames;
            Dictionary<int, string> relatedTenancyNames;

            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                var tenantIds = conversations.Select(c => c.Key.TenantId);
                relatedTenancyNames = _tenantRepository.GetAll().Where(t => tenantIds.Contains(t.Id))
                    .ToDictionary(t => t.Id, t => t.TenancyName);
                relatedUsernames = GetFriendUsernames(conversations.Select(c => c.Key).ToList());
            }

            var chatMessageFiles = new List<FileDto>();

            foreach (var conversation in conversations)
            {
                foreach (var message in conversation.Value)
                {
                    message.TargetTenantName = message.TargetTenantId.HasValue
                        ? relatedTenancyNames[message.TargetTenantId.Value]
                        : ".";

                    message.TargetUserName =
                        relatedUsernames[new UserIdentifier(message.TargetTenantId, message.TargetUserId)];
                }

                var messages = conversation.Value.OrderBy(m => m.CreationTime).ToList();
                chatMessageFiles.Add(_chatMessageListExcelExporter.ExportToFile(messages));
            }

            return chatMessageFiles;
        }

        private Dictionary<UserIdentifier, string> GetFriendUsernames(List<UserIdentifier> users)
        {
            var predicate = PredicateBuilder.False<UserAccount>();

            foreach (var user in users)
                predicate = predicate.Or(ua => ua.TenantId == user.TenantId && ua.UserId == user.UserId);

            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var userList = _userAccountRepository.GetAllList(predicate).Select(ua => new
                {
                    ua.TenantId,
                    ua.UserId,
                    ua.UserName
                }).Distinct();

                return userList.ToDictionary(ua => new UserIdentifier(ua.TenantId, ua.UserId), ua => ua.UserName);
            }
        }

        private async Task<Dictionary<UserIdentifier, List<ChatMessageExportDto>>> GetUserChatMessages(int? tenantId,
            long userId)
        {
            var conversations = await (from message in _chatMessageRepository.GetAll()
                where message.UserId == userId && message.TenantId == tenantId
                group message by new {message.TargetTenantId, message.TargetUserId}
                into messageGrouped
                select new
                {
                    messageGrouped.Key.TargetTenantId,
                    messageGrouped.Key.TargetUserId,
                    Messages = messageGrouped
                }).ToListAsync();

            return conversations.ToDictionary(c => new UserIdentifier(c.TargetTenantId, c.TargetUserId),
                c => c.Messages.MapTo<List<ChatMessageExportDto>>());
        }
    }
}