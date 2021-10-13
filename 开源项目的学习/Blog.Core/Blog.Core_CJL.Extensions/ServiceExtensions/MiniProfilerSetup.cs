using Blog.Core_CJL.Common;
using Blog.Core_CJL.Common.Helper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Extensions.ServiceExtensions
{

    /// <summary>
    /// MiniProfiler 启动服务
    /// </summary>
    public static class MiniProfilerSetup
    {
        public static void AddMiniProfilerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if(Appsettings.app(new string[] { "Startup", "MiniProfiler", "Enabled" }).ObjToBool())
            {
                //添加迷你性能检测
                services.AddMiniProfiler();
            }
        }
    }
}
