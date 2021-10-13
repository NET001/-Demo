using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Tasks
{
    public class JobFactory : IJobFactory
    {
        /// <summary>
        /// 注入反射获取依赖对象
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 实现接口Job
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                //解析作用域服务
                var serviceScope = _serviceProvider.CreateScope();
                //获得指定类型的服务对象
                var job = serviceScope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                return job;

            }
            catch (Exception)
            {
                throw;
            }
        }
        //注销作业
        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }

        }
    }
}
