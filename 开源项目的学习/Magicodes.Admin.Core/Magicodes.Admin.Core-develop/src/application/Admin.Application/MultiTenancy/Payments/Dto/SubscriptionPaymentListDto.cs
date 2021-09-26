// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : SubscriptionPaymentListDto.cs
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

using Abp.Application.Services.Dto;

namespace Magicodes.Admin.Application.MultiTenancy.Payments.Dto
{
    public class SubscriptionPaymentListDto : AuditedEntityDto
    {
        public string Gateway { get; set; }

        public decimal Amount { get; set; }

        public int EditionId { get; set; }

        public int DayCount { get; set; }

        public string PaymentPeriodType { get; set; }

        public string PaymentId { get; set; }

        public string PayerId { get; set; }

        public string Status { get; set; }

        public string EditionDisplayName { get; set; }

        public int TenantId { get; set; }

        public string InvoiceNo { get; set; }
    }
}