// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UserLinkManager.cs
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
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.UI;
using Magicodes.Admin.Core.Authorization.Impersonation;

namespace Magicodes.Admin.Core.Authorization.Users
{
    public class UserLinkManager : AdminDomainServiceBase, IUserLinkManager
    {
        private readonly ICacheManager _cacheManager;
        private readonly UserClaimsPrincipalFactory _principalFactory;
        private readonly IRepository<UserAccount, long> _userAccountRepository;
        private readonly UserManager _userManager;

        public UserLinkManager(
            IRepository<UserAccount, long> userAccountRepository,
            ICacheManager cacheManager,
            UserManager userManager,
            UserClaimsPrincipalFactory principalFactory)
        {
            _userAccountRepository = userAccountRepository;
            _cacheManager = cacheManager;
            _userManager = userManager;
            _principalFactory = principalFactory;

            AbpSession = NullAbpSession.Instance;
        }

        public IAbpSession AbpSession { get; set; }

        [UnitOfWork]
        public virtual async Task Link(User firstUser, User secondUser)
        {
            var firstUserAccount = await GetUserAccountAsync(firstUser.ToUserIdentifier());
            var secondUserAccount = await GetUserAccountAsync(secondUser.ToUserIdentifier());

            var userLinkId = firstUserAccount.UserLinkId ?? firstUserAccount.Id;
            firstUserAccount.UserLinkId = userLinkId;

            var userAccountsToLink = secondUserAccount.UserLinkId.HasValue
                ? _userAccountRepository.GetAllList(ua => ua.UserLinkId == secondUserAccount.UserLinkId.Value)
                : new List<UserAccount> {secondUserAccount};

            userAccountsToLink.ForEach(u => { u.UserLinkId = userLinkId; });

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [UnitOfWork]
        public virtual async Task<bool> AreUsersLinked(UserIdentifier firstUserIdentifier,
            UserIdentifier secondUserIdentifier)
        {
            var firstUserAccount = await GetUserAccountAsync(firstUserIdentifier);
            var secondUserAccount = await GetUserAccountAsync(secondUserIdentifier);

            if (!firstUserAccount.UserLinkId.HasValue || !secondUserAccount.UserLinkId.HasValue) return false;

            return firstUserAccount.UserLinkId == secondUserAccount.UserLinkId;
        }

        [UnitOfWork]
        public virtual async Task Unlink(UserIdentifier userIdentifier)
        {
            var targetUserAccount = await GetUserAccountAsync(userIdentifier);
            targetUserAccount.UserLinkId = null;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [UnitOfWork]
        public virtual async Task<UserAccount> GetUserAccountAsync(UserIdentifier userIdentifier)
        {
            return await _userAccountRepository.FirstOrDefaultAsync(ua =>
                ua.TenantId == userIdentifier.TenantId && ua.UserId == userIdentifier.UserId);
        }

        public async Task<string> GetAccountSwitchToken(long targetUserId, int? targetTenantId)
        {
            //Create a cache item
            var cacheItem = new SwitchToLinkedAccountCacheItem(
                targetTenantId,
                targetUserId,
                AbpSession.ImpersonatorTenantId,
                AbpSession.ImpersonatorUserId
            );

            //Create a random token and save to the cache
            var token = Guid.NewGuid().ToString();

            await _cacheManager
                .GetSwitchToLinkedAccountCache()
                .SetAsync(token, cacheItem, TimeSpan.FromMinutes(1));

            return token;
        }

        public async Task<UserAndIdentity> GetSwitchedUserAndIdentity(string switchAccountToken)
        {
            var cacheItem = await _cacheManager.GetSwitchToLinkedAccountCache().GetOrDefaultAsync(switchAccountToken);
            if (cacheItem == null) throw new UserFriendlyException(L("SwitchToLinkedAccountTokenErrorMessage"));

            //Get the user from tenant
            var user = await _userManager.FindByIdAsync(cacheItem.TargetUserId.ToString());

            //Create identity
            var identity = (ClaimsIdentity) (await _principalFactory.CreateAsync(user)).Identity;

            //Add claims for audit logging
            if (cacheItem.ImpersonatorTenantId.HasValue)
                identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorTenantId,
                    cacheItem.ImpersonatorTenantId.Value.ToString(CultureInfo.InvariantCulture)));

            if (cacheItem.ImpersonatorUserId.HasValue)
                identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorUserId,
                    cacheItem.ImpersonatorUserId.Value.ToString(CultureInfo.InvariantCulture)));

            //Remove the cache item to prevent re-use
            await _cacheManager.GetSwitchToLinkedAccountCache().RemoveAsync(switchAccountToken);

            return new UserAndIdentity(user, identity);
        }
    }
}