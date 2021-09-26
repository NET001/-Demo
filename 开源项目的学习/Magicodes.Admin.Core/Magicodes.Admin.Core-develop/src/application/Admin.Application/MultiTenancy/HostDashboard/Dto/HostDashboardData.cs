// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : HostDashboardData.cs
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
using System.Collections.Generic;

namespace Magicodes.Admin.Application.MultiTenancy.HostDashboard.Dto
{
    public class HostDashboardData
    {
        public int NewTenantsCount { get; set; }
        public decimal NewSubscriptionAmount { get; set; }
        public int DashboardPlaceholder1 { get; set; }
        public int DashboardPlaceholder2 { get; set; }
        public List<IncomeStastistic> IncomeStatistics { get; set; }
        public List<TenantEdition> EditionStatistics { get; set; }
        public List<ExpiringTenant> ExpiringTenants { get; set; }
        public List<RecentTenant> RecentTenants { get; set; }
        public int MaxExpiringTenantsShownCount { get; set; }
        public int MaxRecentTenantsShownCount { get; set; }
        public int SubscriptionEndAlertDayCount { get; set; }
        public int RecentTenantsDayCount { get; set; }
        public DateTime SubscriptionEndDateStart { get; set; }
        public DateTime SubscriptionEndDateEnd { get; set; }
        public DateTime TenantCreationStartDate { get; set; }
    }
}