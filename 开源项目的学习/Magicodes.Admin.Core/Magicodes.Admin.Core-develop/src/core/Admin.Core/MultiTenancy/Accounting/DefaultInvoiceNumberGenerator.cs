// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : DefaultInvoiceNumberGenerator.cs
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
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Timing;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.Core.MultiTenancy.Accounting
{
    public class DefaultInvoiceNumberGenerator : IInvoiceNumberGenerator
    {
        private readonly IRepository<Invoice> _invoiceRepository;

        public DefaultInvoiceNumberGenerator(IRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [UnitOfWork]
        public async Task<string> GetNewInvoiceNumber()
        {
            var lastInvoice = await _invoiceRepository.GetAll().OrderByDescending(i => i.Id).FirstOrDefaultAsync();
            if (lastInvoice == null) return Clock.Now.Year + "" + Clock.Now.Month.ToString("00") + "00001";

            var year = Convert.ToInt32(lastInvoice.InvoiceNo.Substring(0, 4));
            var month = Convert.ToInt32(lastInvoice.InvoiceNo.Substring(4, 2));

            var invoiceNumberToIncrease = lastInvoice.InvoiceNo.Substring(6, lastInvoice.InvoiceNo.Length - 6);
            if (year != Clock.Now.Year || month != Clock.Now.Month) invoiceNumberToIncrease = "0";

            var invoiceNumberPostfix = Convert.ToInt32(invoiceNumberToIncrease) + 1;
            return Clock.Now.Year + Clock.Now.Month.ToString("00") + invoiceNumberPostfix.ToString("00000");
        }
    }
}