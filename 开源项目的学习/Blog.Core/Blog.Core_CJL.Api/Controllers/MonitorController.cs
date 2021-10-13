using Blog.Core_CJL.Common;
using Blog.Core_CJL.Common.Helper;
using Blog.Core_CJL.Common.LogHelper;
using Blog.Core_CJL.Extensions.Middlewares;
using Blog.Core_CJL.Model;
using Blog.Core_CJL.Model.MessageCore;
using Blog.Core_CJL.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Api.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class MonitorController : BaseApiCpntroller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<MonitorController> _logger;
        public MonitorController(IWebHostEnvironment env, ILogger<MonitorController> logger)
        {
            _env = env;
            _logger = logger;
        }
        /// <summary>
        /// 服务器配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<ServerViewModel> Server()
        {
            return Success(new ServerViewModel()
            {
                EnvironmentName = _env.EnvironmentName,
                OSArchitecture = RuntimeInformation.OSArchitecture.ObjToString(),
                ContentRootPath = _env.ContentRootPath,
                WebRootPath = _env.WebRootPath,
                FrameworkDescription = RuntimeInformation.FrameworkDescription,
                MemoryFootprint = (Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + " MB",
                WorkingTime = DateHelper.TimeSubTract(DateTime.Now, Process.GetCurrentProcess().StartTime)
            });
        }
        [HttpGet]
        public MessageModel<RequestApiWeekView> GetRequestApiinfoByWeek()
        {
            return Success(LogLock.RequestApiinfoByWeek());
        }
        [HttpGet]
        public MessageModel<AccessApiDateView> GetAccessApiByDate()
        {
            return new MessageModel<AccessApiDateView>()
            {
                msg = "获取成功",
                success = true,
                response = LogLock.AccessApiByDate()
            };
        }
        [HttpGet]
        public MessageModel<AccessApiDateView> GetAccessApiByHour()
        {
            return new MessageModel<AccessApiDateView>()
            {
                msg = "获取成功",
                success = true,
                response = LogLock.AccessApiByHour()
            };
        }
        [HttpGet]
        public MessageModel<WelcomeInitData> GetActiveUsers([FromServices] IWebHostEnvironment environment)
        {
            var accessLogsToday = JsonConvert.DeserializeObject<List<UserAccessModel>>("[" + LogLock.ReadLog(
                Path.Combine(environment.ContentRootPath, "Log"), "RecordAccessLogs_", Encoding.UTF8, ReadType.PrefixLatest
                ) + "]")
                .Where(d => d.BeginTime.ObjToDate() >= DateTime.Today);
            var Logs = accessLogsToday.OrderByDescending(d => d.BeginTime).Take(50).ToList();
            var errorCountToday = LogLock.GetLogData().Where(d => d.Import == 9).Count();
            accessLogsToday = accessLogsToday.Where(d => d.User != "").ToList();
            var activeUsers = (from n in accessLogsToday
                               group n by new { n.User } into g
                               select new ActiveUserVM
                               {
                                   user = g.Key.User,
                                   count = g.Count(),
                               }).ToList();
            int activeUsersCount = activeUsers.Count;
            activeUsers = activeUsers.OrderByDescending(d => d.count).Take(10).ToList();
            return new MessageModel<WelcomeInitData>()
            {
                msg = "获取成功",
                success = true,
                response = new WelcomeInitData()
                {
                    activeUsers = activeUsers,
                    activeUserCount = activeUsersCount,
                    errorCount = errorCountToday,
                    logs = Logs
                }
            };
        }
    }
    public class WelcomeInitData
    {
        public List<ActiveUserVM> activeUsers { get; set; }
        public int activeUserCount { get; set; }
        public List<UserAccessModel> logs { get; set; }
        public int errorCount { get; set; }
    }
}
