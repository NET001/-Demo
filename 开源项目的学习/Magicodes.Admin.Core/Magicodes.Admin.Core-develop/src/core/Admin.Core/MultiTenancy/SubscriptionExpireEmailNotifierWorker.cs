// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : SubscriptionExpireEmailNotifierWorker.cs
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

using System;
using System.Diagnostics;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.Configuration;
using Magicodes.Admin.Localization;

namespace Magicodes.Admin.Core.MultiTenancy
{
    public class SubscriptionExpireEmailNotifierWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private const int CheckPeriodAsMilliseconds = 1 * 60 * 60 * 1000 * 24; //1 day

        private readonly IRepository<Tenant> _tenantRepository;
        private readonly UserEmailer _userEmailer;

        public SubscriptionExpireEmailNotifierWorker(
            AbpTimer timer,
            IRepository<Tenant> tenantRepository,
            UserEmailer userEmailer) : base(timer)
        {
            _tenantRepository = tenantRepository;
            _userEmailer = userEmailer;

            Timer.Period = CheckPeriodAsMilliseconds;
            Timer.RunOnStart = true;

            LocalizationSourceName = LocalizationConsts.LocalizationSourceName;
        }

        protected override void DoWork()
        {
            var subscriptionRemainingDayCount =
                Convert.ToInt32(
                    SettingManager.GetSettingValueForApplication(AppSettings.TenantManagement
                        .SubscriptionExpireNotifyDayCount));
            var dateToCheckRemainingDayCount = Clock.Now.AddDays(subscriptionRemainingDayCount).ToUniversalTime();

            var subscriptionExpiredTenants = _tenantRepository.GetAllList(
                tenant => tenant.SubscriptionEndDateUtc != null &&
                          tenant.SubscriptionEndDateUtc.Value.Date == dateToCheckRemainingDayCount.Date &&
                          tenant.IsActive &&
                          tenant.EditionId != null
            );

            foreach (var tenant in subscriptionExpiredTenants)
            {
                Debug.Assert(tenant.EditionId.HasValue);
                try
                {
                    _userEmailer.TryToSendSubscriptionExpiringSoonEmail(tenant.Id, dateToCheckRemainingDayCount);
                }
                catch (Exception exception)
                {
                    Logger.Error(exception.Message, exception);
                }
            }
        }
    }
}