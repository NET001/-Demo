// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : SessionAppService.cs
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Runtime.Session;
using Magicodes.Admin.Application.Core;
using Magicodes.Admin.Application.Sessions.Dto;
using Magicodes.Admin.Core;
using Magicodes.Admin.Core.Configuration;
using Magicodes.Admin.Core.Editions;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.Application.Sessions
{
    public class SessionAppService : AdminAppServiceBase, ISessionAppService
    {
        public IAppConfigurationAccessor AppConfigurationAccessor { get; set; }

        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var configuration = AppConfigurationAccessor.Configuration;
            var releaseDate = configuration["CustomInfo:ReleaseDate"] != null
                ? Convert.ToDateTime(configuration["CustomInfo:ReleaseDate"])
                : AppVersionHelper.ReleaseDate;
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = configuration["CustomInfo:Version"] ?? AppVersionHelper.Version,
                    ReleaseDate = releaseDate,
                    Features = new Dictionary<string, bool>(),
                    Name = configuration["CustomInfo:Name"] ?? "Magicodes.Admin"
                }
            };

            if (AbpSession.TenantId.HasValue)
                output.Tenant = ObjectMapper
                    .Map<TenantLoginInfoDto>(await TenantManager
                        .Tenants
                        .Include(t => t.Edition)
                        .FirstAsync(t => t.Id == AbpSession.GetTenantId()));

            if (AbpSession.UserId.HasValue)
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());

            if (output.Tenant == null) return output;

            if (output.Tenant.Edition != null)
                output.Tenant.Edition.IsHighestEdition = IsEditionHighest(output.Tenant.Edition.Id);

            output.Tenant.SubscriptionDateString = GetTenantSubscriptionDateString(output);
            {
                output.Tenant.CreationTimeString = output.Tenant.CreationTime.ToString("d");

                return output;
            }
        }

        public async Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken()
        {
            if (AbpSession.UserId <= 0) throw new Exception(L("ThereIsNoLoggedInUser"));

            var user = await UserManager.GetUserAsync(AbpSession.ToUserIdentifier());
            user.SetSignInToken();
            return new UpdateUserSignInTokenOutput
            {
                SignInToken = user.SignInToken,
                EncodedUserId = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id.ToString())),
                EncodedTenantId = user.TenantId.HasValue
                    ? Convert.ToBase64String(Encoding.UTF8.GetBytes(user.TenantId.Value.ToString()))
                    : ""
            };
        }

        private bool IsEditionHighest(int editionId)
        {
            var topEdition = GetHighestEditionOrNullByMonthlyPrice();
            if (topEdition == null) return false;

            return editionId == topEdition.Id;
        }

        private SubscribableEdition GetHighestEditionOrNullByMonthlyPrice()
        {
            var editions = TenantManager.EditionManager.Editions;
            if (editions == null || !editions.Any()) return null;

            return editions.Cast<SubscribableEdition>()
                .OrderByDescending(e => e.MonthlyPrice)
                .FirstOrDefault();
        }

        private string GetTenantSubscriptionDateString(GetCurrentLoginInformationsOutput output)
        {
            return output.Tenant.SubscriptionEndDateUtc == null
                ? L("Unlimited")
                : output.Tenant.SubscriptionEndDateUtc?.ToString("d");
        }
    }
}