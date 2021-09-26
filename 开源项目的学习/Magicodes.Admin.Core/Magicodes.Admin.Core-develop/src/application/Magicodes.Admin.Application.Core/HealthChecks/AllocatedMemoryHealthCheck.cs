using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Magicodes.Admin.Application.Core.HealthChecks
{
    /// <summary>
    /// 内存检查
    /// </summary>
    public class AllocatedMemoryHealthCheck : IHealthCheck
    {
        private readonly int _maximumMegabytesAllocated = 150;
        private readonly IConfiguration _appConfiguration;

        /// <summary>
        /// 
        /// </summary>
        public AllocatedMemoryHealthCheck(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
            if (!appConfiguration["HealthChecks:MemoryThreshold"].IsNullOrEmpty())
            {
                _maximumMegabytesAllocated = Convert.ToInt32(appConfiguration["HealthChecks:MemoryThreshold"]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var totalMemoryBytes = GC.GetTotalMemory(false) / 1024 / 1024;

            if (totalMemoryBytes >= _maximumMegabytesAllocated)
            {
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus,
                        description: $"内存超过阈值: {totalMemoryBytes} mb / {_maximumMegabytesAllocated} mb"));
            }

            return Task.FromResult(HealthCheckResult.Healthy(description: $"当前内存占用: {totalMemoryBytes} mb"));

        }
    }
}
