// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AdminDbContextConfigurer.cs
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

using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.EntityFrameworkCore.EntityFramework
{
    public static class AdminDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AdminDbContext> builder, string connectionString,
            bool isUseRowNumber = true)
        {
            if (isUseRowNumber)
                //以支持SQL Server 2012以下数据库
                builder.UseSqlServer(connectionString, p => p.UseRowNumberForPaging());
            else
                builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AdminDbContext> builder, DbConnection connection,
            bool isUseRowNumber = true)
        {
            if (isUseRowNumber)
                //以支持SQL Server 2012以下数据库
                builder.UseSqlServer(connection, p => p.UseRowNumberForPaging());
            else
                builder.UseSqlServer(connection);
        }
    }
}