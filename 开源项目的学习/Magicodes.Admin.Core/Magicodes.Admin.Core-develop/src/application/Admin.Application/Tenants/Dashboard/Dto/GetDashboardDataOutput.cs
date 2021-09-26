// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : GetDashboardDataOutput.cs
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

namespace Magicodes.Admin.Application.Tenants.Dashboard.Dto
{
    public class GetDashboardDataOutput
    {
        public int TotalProfit { get; set; }

        public int NewFeedbacks { get; set; }

        public int NewOrders { get; set; }

        public int NewUsers { get; set; }

        public List<SalesSummaryData> SalesSummary { get; set; }

        public int TotalSales { get; set; }

        public int Revenue { get; set; }

        public int Expenses { get; set; }

        public int Growth { get; set; }

        public int TransactionPercent { get; set; }


        public int NewVisitPercent { get; set; }

        public int BouncePercent { get; set; }

        public int[] DailySales { get; set; }

        public int[] ProfitShares { get; set; }
    }
}