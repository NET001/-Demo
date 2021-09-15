using Blog.CoreTest.Common;
using log4net;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Extensions.Middlewares
{
    public static class MiniProfilerMildd
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MiniProfilerMildd));
        public static void UseMiniProfilerMildd(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            try
            {
                if (Appsettings.app("Startup", "MiniProfiler", "Enabled").ObjToBool())
                {
                    // 性能分析
                    app.UseMiniProfiler();
                }
            }
            catch (Exception e)
            {
                log.Error($"An error was reported when starting the MiniProfilerMildd.\n{e.Message}");
                throw;
            }
        }
    }
}
