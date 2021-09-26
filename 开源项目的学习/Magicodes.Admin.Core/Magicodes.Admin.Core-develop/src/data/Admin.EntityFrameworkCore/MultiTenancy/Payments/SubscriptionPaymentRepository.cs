// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : SubscriptionPaymentRepository.cs
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

using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using Magicodes.Admin.Core.MultiTenancy.Payments;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework.Repositories;

namespace Magicodes.Admin.EntityFrameworkCore.MultiTenancy.Payments
{
    public class SubscriptionPaymentRepository : AdminRepositoryBase<SubscriptionPayment, long>,
        ISubscriptionPaymentRepository
    {
        public SubscriptionPaymentRepository(IDbContextProvider<AdminDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<SubscriptionPayment> UpdateByGatewayAndPaymentIdAsync(SubscriptionPaymentGatewayType gateway,
            string paymentId, int? tenantId, SubscriptionPaymentStatus status)
        {
            var payment = await SingleAsync(p => p.PaymentId == paymentId && p.Gateway == gateway);

            payment.Status = status;

            if (tenantId.HasValue) payment.TenantId = tenantId.Value;

            return payment;
        }

        public async Task<SubscriptionPayment> GetByGatewayAndPaymentIdAsync(SubscriptionPaymentGatewayType gateway,
            string paymentId)
        {
            return await SingleAsync(p => p.PaymentId == paymentId && p.Gateway == gateway);
        }
    }
}