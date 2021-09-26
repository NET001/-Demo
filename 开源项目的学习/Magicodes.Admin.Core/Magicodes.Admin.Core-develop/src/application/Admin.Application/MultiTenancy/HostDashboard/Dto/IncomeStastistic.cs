// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : IncomeStastistic.cs
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

namespace Magicodes.Admin.Application.MultiTenancy.HostDashboard.Dto
{
    public class IncomeStastistic
    {
        public IncomeStastistic()
        {
        }

        public IncomeStastistic(DateTime date)
        {
            Date = date;
        }

        public IncomeStastistic(DateTime date, decimal amount)
        {
            Date = date;
            Amount = amount;
        }

        public string Label { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}