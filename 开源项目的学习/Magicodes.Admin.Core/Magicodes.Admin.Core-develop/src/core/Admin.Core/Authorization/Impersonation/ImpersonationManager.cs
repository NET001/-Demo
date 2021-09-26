// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ImpersonationManager.cs
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
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.UI;
using Magicodes.Admin.Core.Authorization.Users;

namespace Magicodes.Admin.Core.Authorization.Impersonation
{
    public class ImpersonationManager : AdminDomainServiceBase, IImpersonationManager
    {
        private readonly ICacheManager _cacheManager;
        private readonly UserClaimsPrincipalFactory _principalFactory;
        private readonly UserManager _userManager;

        public ImpersonationManager(
            ICacheManager cacheManager,
            UserManager userManager,
            UserClaimsPrincipalFactory principalFactory)
        {
            _cacheManager = cacheManager;
            _userManager = userManager;
            _principalFactory = principalFactory;

            AbpSession = NullAbpSession.Instance;
        }

        public IAbpSession AbpSession { get; set; }

        public async Task<UserAndIdentity> GetImpersonatedUserAndIdentity(string impersonationToken)
        {
            var cacheItem = await _cacheManager.GetImpersonationCache().GetOrDefaultAsync(impersonationToken);
            if (cacheItem == null) throw new UserFriendlyException(L("ImpersonationTokenErrorMessage"));

            //TODO:docker上AbpSession.TenantId取到为null,所以注释掉了验证,启用前台传过来的TenantId

            //CheckCurrentTenant(cacheItem.TargetTenantId);

            using (CurrentUnitOfWork.SetTenantId(cacheItem.TargetTenantId))
            {
                //Get the user from tenant
                var user = await _userManager.FindByIdAsync(cacheItem.TargetUserId.ToString());

                //Create identity

                var identity = (ClaimsIdentity) (await _principalFactory.CreateAsync(user)).Identity;

                if (!cacheItem.IsBackToImpersonator)
                {
                    //Add claims for audit logging
                    if (cacheItem.ImpersonatorTenantId.HasValue)
                        identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorTenantId,
                            cacheItem.ImpersonatorTenantId.Value.ToString(CultureInfo.InvariantCulture)));

                    identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorUserId,
                        cacheItem.ImpersonatorUserId.ToString(CultureInfo.InvariantCulture)));
                }

                //Remove the cache item to prevent re-use
                await _cacheManager.GetImpersonationCache().RemoveAsync(impersonationToken);

                return new UserAndIdentity(user, identity);
            }
        }

        public Task<string> GetImpersonationToken(long userId, int? tenantId)
        {
            if (AbpSession.ImpersonatorUserId.HasValue)
                throw new UserFriendlyException(L("CascadeImpersonationErrorMessage"));

            if (AbpSession.TenantId.HasValue)
            {
                if (!tenantId.HasValue) throw new UserFriendlyException(L("FromTenantToHostImpersonationErrorMessage"));

                if (tenantId.Value != AbpSession.TenantId.Value)
                    throw new UserFriendlyException(L("DifferentTenantImpersonationErrorMessage"));
            }

            return GenerateImpersonationTokenAsync(tenantId, userId, false);
        }

        public Task<string> GetBackToImpersonatorToken()
        {
            if (!AbpSession.ImpersonatorUserId.HasValue)
                throw new UserFriendlyException(L("NotImpersonatedLoginErrorMessage"));

            return GenerateImpersonationTokenAsync(AbpSession.ImpersonatorTenantId, AbpSession.ImpersonatorUserId.Value,
                true);
        }

        private void CheckCurrentTenant(int? tenantId)
        {
            if (AbpSession.TenantId != tenantId)
                throw new Exception(
                    $"Current tenant is different than given tenant. AbpSession.TenantId: {AbpSession.TenantId}, given tenantId: {tenantId}");
        }

        private async Task<string> GenerateImpersonationTokenAsync(int? tenantId, long userId,
            bool isBackToImpersonator)
        {
            //Create a cache item
            var cacheItem = new ImpersonationCacheItem(
                tenantId,
                userId,
                isBackToImpersonator
            );

            if (!isBackToImpersonator)
            {
                cacheItem.ImpersonatorTenantId = AbpSession.TenantId;
                cacheItem.ImpersonatorUserId = AbpSession.GetUserId();
            }

            //Create a random token and save to the cache
            var token = Guid.NewGuid().ToString();

            await _cacheManager
                .GetImpersonationCache()
                .SetAsync(token, cacheItem, TimeSpan.FromMinutes(1));

            return token;
        }
    }
}