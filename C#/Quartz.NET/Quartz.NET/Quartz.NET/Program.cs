using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace QuartzTest.NET
{
    public class Program
    {
        static void Main(string[] args)
        {
            Demo1();
        }
        static async void Demo1()
        {
            // construct a scheduler factory using defaults
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // 任务
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // 定时器
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(40)
                    .RepeatForever())
            .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
