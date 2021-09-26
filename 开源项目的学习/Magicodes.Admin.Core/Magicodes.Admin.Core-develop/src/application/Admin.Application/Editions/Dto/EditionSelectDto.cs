// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : EditionSelectDto.cs
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
using Magicodes.Admin.Core.MultiTenancy.Payments;

namespace Magicodes.Admin.Application.Editions.Dto
{
    public class EditionSelectDto
    {
        public EditionSelectDto()
        {
            AdditionalData = new Dictionary<SubscriptionPaymentGatewayType, Dictionary<string, string>>();
        }

        public int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public int? ExpiringEditionId { get; set; }

        public decimal? MonthlyPrice { get; set; }

        public decimal? AnnualPrice { get; set; }

        public int? TrialDayCount { get; set; }

        public int? WaitingDayAfterExpire { get; set; }

        public bool IsFree { get; set; }

        public Dictionary<SubscriptionPaymentGatewayType, Dictionary<string, string>> AdditionalData { get; set; }
    }
}