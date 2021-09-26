// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AuditLogAppService_Tests.cs
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

using System.Linq;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Timing;
using Magicodes.Admin.Application.Auditing;
using Magicodes.Admin.Application.Auditing.Dto;
using Shouldly;
using Xunit;

namespace Magicodes.Admin.Tests.Auditing
{
    public class AuditLogAppService_Tests : AppTestBase
    {
        public AuditLogAppService_Tests()
        {
            _auditLogAppService = Resolve<IAuditLogAppService>();
        }

        private readonly IAuditLogAppService _auditLogAppService;

        [Fact]
        public async Task Should_Get_Audit_Logs()
        {
            //Arrange
            UsingDbContext(
                context =>
                {
                    context.AuditLogs.Add(
                        new AuditLog
                        {
                            TenantId = AbpSession.TenantId,
                            UserId = AbpSession.UserId,
                            ServiceName = "ServiceName-Test-1",
                            MethodName = "MethodName-Test-1",
                            Parameters = "{}",
                            ExecutionTime = Clock.Now.AddMinutes(-1),
                            ExecutionDuration = 123
                        });

                    context.AuditLogs.Add(
                        new AuditLog
                        {
                            TenantId = AbpSession.TenantId,
                            ServiceName = "ServiceName-Test-2",
                            MethodName = "MethodName-Test-2",
                            Parameters = "{}",
                            ExecutionTime = Clock.Now,
                            ExecutionDuration = 456
                        });
                });

            //Act
            var output = await _auditLogAppService.GetAuditLogs(new GetAuditLogsInput
            {
                StartDate = Clock.Now.AddMinutes(-10),
                EndDate = Clock.Now.AddMinutes(10)
            });

            output.TotalCount.ShouldBe(2);

            output.Items[0].ServiceName.ShouldBe("ServiceName-Test-2");
            output.Items[0].UserName.ShouldBe(null);

            output.Items[1].ServiceName.ShouldBe("ServiceName-Test-1");

            output.Items[1].UserName.ShouldBe(AbpUserBase.AdminUserName, StringCompareShould.IgnoreCase);
        }

        [Fact]
        public async Task Should_Get_Entity_Changes()
        {
            LoginAsHostAdmin();

            //Arrange
            UsingDbContext(
                context =>
                {
                    var aTenant = context.Tenants.FirstOrDefault();

                    aTenant.Name = "changed name";

                    var aUser = context.Users.FirstOrDefault(u => u.TenantId == null);

                    if (aUser != null) aUser.Name = "changed name";

                    context.SaveChanges();
                });

            //Act
            var entityChangeList = await _auditLogAppService.GetEntityChanges(new GetEntityChangeInput
            {
                StartDate = Clock.Now.AddMinutes(-10),
                EndDate = Clock.Now.AddMinutes(10)
            });

            entityChangeList.TotalCount.ShouldBe(2);
        }
    }
}