// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : Tenant.cs
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
using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;
using Abp.Timing;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.Editions;
using Magicodes.Admin.Core.MultiTenancy.Payments;

namespace Magicodes.Admin.Core.MultiTenancy
{
    /// <summary>
    ///     Represents a Tenant in the system.
    ///     A tenant is a isolated customer for the application
    ///     which has it's own users, roles and other application entities.
    /// </summary>
    public class Tenant : AbpTenant<User>
    {
        public const int MaxLogoMimeTypeLength = 64;

        protected Tenant()
        {
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }

        //Can add application specific tenant properties here

        public DateTime? SubscriptionEndDateUtc { get; set; }

        public bool IsInTrialPeriod { get; set; }

        public virtual Guid? CustomCssId { get; set; }

        public virtual Guid? LogoId { get; set; }

        [MaxLength(MaxLogoMimeTypeLength)] public virtual string LogoFileType { get; set; }

        public virtual bool HasLogo()
        {
            return LogoId != null && LogoFileType != null;
        }

        public void ClearLogo()
        {
            LogoId = null;
            LogoFileType = null;
        }

        public void UpdateSubscriptionDateForPayment(PaymentPeriodType paymentPeriodType,
            EditionPaymentType editionPaymentType)
        {
            switch (editionPaymentType)
            {
                case EditionPaymentType.NewRegistration:
                case EditionPaymentType.BuyNow:
                {
                    SubscriptionEndDateUtc = Clock.Now.ToUniversalTime().AddDays((int) paymentPeriodType);
                    break;
                }

                case EditionPaymentType.Extend:
                    ExtendSubscriptionDate(paymentPeriodType);
                    break;
                case EditionPaymentType.Upgrade:
                    if (HasUnlimitedTimeSubscription())
                        SubscriptionEndDateUtc = Clock.Now.ToUniversalTime().AddDays((int) paymentPeriodType);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private void ExtendSubscriptionDate(PaymentPeriodType paymentPeriodType)
        {
            if (SubscriptionEndDateUtc == null)
                throw new InvalidOperationException("Can not extend subscription date while it's null!");

            if (IsSubscriptionEnded()) SubscriptionEndDateUtc = Clock.Now.ToUniversalTime();

            SubscriptionEndDateUtc = SubscriptionEndDateUtc.Value.AddDays((int) paymentPeriodType);
        }

        private bool IsSubscriptionEnded()
        {
            return SubscriptionEndDateUtc < Clock.Now.ToUniversalTime();
        }

        public int CalculateRemainingDayCount()
        {
            return SubscriptionEndDateUtc != null
                ? (SubscriptionEndDateUtc.Value - Clock.Now.ToUniversalTime()).Days
                : 0;
        }

        public bool HasUnlimitedTimeSubscription()
        {
            return SubscriptionEndDateUtc == null;
        }
    }
}