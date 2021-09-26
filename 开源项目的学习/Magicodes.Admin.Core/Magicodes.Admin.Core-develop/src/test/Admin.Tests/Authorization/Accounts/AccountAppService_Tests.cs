// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AccountAppService_Tests.cs
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
using Abp.MultiTenancy;
using Magicodes.Admin.Application.Authorization.Accounts;
using Magicodes.Admin.Application.Authorization.Accounts.Dto;
using Shouldly;
using Xunit;

namespace Magicodes.Admin.Tests.Authorization.Accounts
{
    public class AccountAppService_Tests : AppTestBase
    {
        public AccountAppService_Tests()
        {
            _accountAppService = Resolve<IAccountAppService>();
        }

        private readonly IAccountAppService _accountAppService;

        [Fact]
        public async Task Should_Check_If_Given_Tenant_Is_Available()
        {
            //Act
            var output = await _accountAppService.IsTenantAvailable(
                new IsTenantAvailableInput
                {
                    TenancyName = AbpTenantBase.DefaultTenantName
                }
            );

            //Assert
            output.State.ShouldBe(TenantAvailabilityState.Available);
            output.TenantId.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Register()
        {
            //Act
            await _accountAppService.Register(new RegisterInput
            {
                UserName = "john",
                Password = "john123",
                Name = "John",
                Surname = "Nash",
                EmailAddress = "xinlai@xin-lai.com"
            });

            //Assert
            UsingDbContext(context =>
            {
                context.Users.FirstOrDefault(
                    u => u.TenantId == AbpSession.TenantId &&
                         u.EmailAddress == "xinlai@xin-lai.com"
                ).ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Return_NotFound_If_Tenant_Is_Not_Defined()
        {
            //Act
            var output = await _accountAppService.IsTenantAvailable(
                new IsTenantAvailableInput
                {
                    TenancyName = "UnknownTenant"
                }
            );

            //Assert
            output.State.ShouldBe(TenantAvailabilityState.NotFound);
        }
    }
}