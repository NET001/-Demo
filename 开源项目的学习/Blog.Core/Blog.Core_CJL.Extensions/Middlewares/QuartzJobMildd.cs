using Blog.Core_CJL.Common;
using Blog.Core_CJL.Common.Helper;
using Blog.Core_CJL.IServices;
using Blog.Core_CJL.Tasks;
using log4net;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Extensions.Middlewares
{

    public static class QuartzJobMildd
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(QuartzJobMildd));
        public static void UseQuartzJobMildd(this IApplicationBuilder app, ITasksQzServices tasksQzServices, ISchedulerCenter schedulerCenter)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            try
            {
                if (Appsettings.app("Middleware", "QuartzNetJob", "Enabled").ObjToBool())
                {

                    var allQzServices = tasksQzServices.Query().Result;
                    foreach (var item in allQzServices)
                    {
                        if (item.IsStart)
                        {
                            var ResuleModel = schedulerCenter.AddScheduleJobAsync(item).Result;
                            if (ResuleModel.success)
                            {
                                Console.WriteLine($"QuartzNetJob{item.Name}启动成功！");
                            }
                            else
                            {
                                Console.WriteLine($"QuartzNetJob{item.Name}启动失败！错误信息：{ResuleModel.msg}");
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                log.Error($"An error was reported when starting the job service.\n{e.Message}");
                throw;
            }
        }
    }
}
