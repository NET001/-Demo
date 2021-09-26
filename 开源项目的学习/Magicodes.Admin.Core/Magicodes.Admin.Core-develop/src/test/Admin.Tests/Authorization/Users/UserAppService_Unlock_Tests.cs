// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UserAppService_Unlock_Tests.cs
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

using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.MultiTenancy;
using Magicodes.Admin.Application.Authorization;
using Magicodes.Admin.Core.Authorization.Users;
using Shouldly;
using Xunit;

namespace Magicodes.Admin.Tests.Authorization.Users
{
    public class UserAppService_Unlock_Tests : UserAppServiceTestBase
    {
        public UserAppService_Unlock_Tests()
        {
            _userManager = Resolve<UserManager>();
            _loginManager = Resolve<LogInManager>();

            CreateTestUsers();
        }

        private readonly UserManager _userManager;
        private readonly LogInManager _loginManager;

        [Fact]
        public async Task Should_Unlock_User()
        {
            //Arrange

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
            var user = await GetUserByUserNameAsync("jnash");

            //Pre conditions
            (await _userManager.IsLockedOutAsync(user)).ShouldBeFalse();
            user.IsLockoutEnabled.ShouldBeTrue();

            //Try wrong password until lockout
            AbpLoginResultType loginResultType;
            do
            {
                loginResultType =
                    (await _loginManager.LoginAsync(user.UserName, "wrong-password", AbpTenantBase.DefaultTenantName))
                    .Result;
            } while (loginResultType != AbpLoginResultType.LockedOut);

            (await _userManager.IsLockedOutAsync(await GetUserByUserNameAsync("jnash"))).ShouldBeTrue();

            //Act

            await UserAppService.UnlockUser(new EntityDto<long>(user.Id));

            //Assert

            (await _loginManager.LoginAsync(user.UserName, "wrong-password", AbpTenantBase.DefaultTenantName)).Result
                .ShouldBe(AbpLoginResultType.InvalidPassword);
        }
    }
}