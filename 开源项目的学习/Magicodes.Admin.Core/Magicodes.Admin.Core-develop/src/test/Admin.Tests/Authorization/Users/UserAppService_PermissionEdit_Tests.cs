// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UserAppService_PermissionEdit_Tests.cs
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
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Magicodes.Admin.Application.Authorization.Users.Dto;
using Magicodes.Admin.Core.Authorization;
using Magicodes.Admin.Core.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Magicodes.Admin.Tests.Authorization.Users
{
    public class UserAppService_PermissionEdit_Tests : UserAppServiceTestBase
    {
        [Fact]
        public async Task Should_Get_User_Permissions()
        {
            //Arrange
            var admin = await GetUserByUserNameAsync(AbpUserBase.AdminUserName);

            //Act
            var output = await UserAppService.GetUserPermissionsForEdit(new EntityDto<long>(admin.Id));

            //Assert
            output.GrantedPermissionNames.ShouldNotBe(null);
            output.Permissions.ShouldNotBe(null);
        }

        [Fact]
        public async Task Should_Reset_Permissions()
        {
            //Arrange
            var admin = await GetUserByUserNameAsync(AbpUserBase.AdminUserName);
            UsingDbContext(
                context => context.UserPermissions.Add(
                    new UserPermissionSetting
                    {
                        TenantId = AbpSession.TenantId,
                        UserId = admin.Id,
                        Name = AppPermissions.Pages_Administration_Roles,
                        IsGranted = false
                    }));

            //Act
            await UserAppService.ResetUserSpecificPermissions(new EntityDto<long>(admin.Id));

            //Assert
            (await UsingDbContextAsync(context => context.UserPermissions.CountAsync(p => p.UserId == admin.Id)))
                .ShouldBe(0);
        }

        [Fact]
        public async Task Should_Update_User_Permissions()
        {
            //Arrange
            var admin = await GetUserByUserNameAsync(AbpUserBase.AdminUserName);
            var permissions = Resolve<IPermissionManager>()
                .GetAllPermissions()
                .Where(p => p.MultiTenancySides.HasFlag(AbpSession.MultiTenancySide))
                .ToList();

            //Act
            await UserAppService.UpdateUserPermissions(
                new UpdateUserPermissionsInput
                {
                    Id = admin.Id,
                    GrantedPermissionNames = permissions.Select(p => p.Name).ToList()
                });

            //Assert
            var userManager = Resolve<UserManager>();
            foreach (var permission in permissions)
                (await userManager.IsGrantedAsync(admin, permission)).ShouldBe(true);
        }
    }
}