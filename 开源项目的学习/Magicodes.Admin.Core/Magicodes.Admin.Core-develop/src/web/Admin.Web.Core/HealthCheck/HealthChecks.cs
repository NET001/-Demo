using Magicodes.Admin.Application.Core.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace Magicodes.Admin.Web.Core.HealthCheck
{
    public static class HealthChecks
    {
        public static IHealthChecksBuilder AddAdminHealthChecks(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();
            builder.AddCheck<DbContextHealthCheck>("数据库连接检查");
            builder.AddCheck<DbContextUsersHealthCheck>("数据库连接以及用户检查");
            builder.AddCheck<CacheHealthCheck>("缓存检查");
            builder.AddCheck<AllocatedMemoryHealthCheck>("内存检查");

            return builder;
        }
    }
}
