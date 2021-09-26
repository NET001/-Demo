using System.Threading;
using System.Threading.Tasks;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Magicodes.Admin.Application.Core.HealthChecks
{
    public class DbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public DbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy(nameof(AdminDbContext) + "已连接数据库。"));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy(nameof(AdminDbContext) + "无法连接数据库。"));
        }
    }
}
