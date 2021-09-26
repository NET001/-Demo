// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : HostRoleAndUserCreator.cs
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
using Abp;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Notifications;
using Magicodes.Admin.Core.Authorization;
using Magicodes.Admin.Core.Authorization.Roles;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.Custom.Authorization;
using Magicodes.Admin.Core.Notifications;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.EntityFrameworkCore.Migrations.Seed.Host
{
    public class HostRoleAndUserCreator
    {
        private readonly AdminDbContext _context;

        public HostRoleAndUserCreator(AdminDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles.IgnoreQueryFilters()
                .FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles
                    .Add(new Role(null, StaticRoleNames.Host.Admin, StaticRoleNames.Host.Admin)
                        {IsStatic = true, IsDefault = true}).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to admin role

            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == null && p.RoleId == adminRoleForHost.Id)
                .Select(p => p.Name)
                .ToList();

            var permissions = PermissionFinder
                .GetAllPermissions(
                    new AppAuthorizationProvider(false),
                    new AppCustomAuthorizationProvider(false)
                )
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRoleForHost.Id
                    })
                );
                _context.SaveChanges();
            }

            //admin user for host

            var adminUserForHost = _context.Users.IgnoreQueryFilters()
                .FirstOrDefault(u => u.TenantId == null && u.UserName == AbpUserBase.AdminUserName);
            if (adminUserForHost == null)
            {
                var user = new User
                {
                    TenantId = null,
                    UserName = AbpUserBase.AdminUserName,
                    Name = "admin",
                    Surname = "admin",
                    EmailAddress = "admin@xin-lai.com",
                    IsEmailConfirmed = true,
                    //TODO:为了兼容新版UI先将改为false
                    ShouldChangePasswordOnNextLogin = false,
                    IsActive = true,
                    Password = "AOEdQVs7aP4oMpOItKAValbRCfv4t0hwvYa/fP6k4wi0brAQ0cLcOGjpFxE/2sdIeA==" //123456abcD
                };

                user.SetNormalizedNames();

                adminUserForHost = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();

                //User account of admin user
                _context.UserAccounts.Add(new UserAccount
                {
                    TenantId = null,
                    UserId = adminUserForHost.Id,
                    UserName = AbpUserBase.AdminUserName,
                    EmailAddress = adminUserForHost.EmailAddress
                });

                _context.SaveChanges();

                //Notification subscriptions
                _context.NotificationSubscriptions.Add(new NotificationSubscriptionInfo(
                    SequentialGuidGenerator.Instance.Create(), null, adminUserForHost.Id,
                    AppNotificationNames.NewTenantRegistered));
                _context.NotificationSubscriptions.Add(new NotificationSubscriptionInfo(
                    SequentialGuidGenerator.Instance.Create(), null, adminUserForHost.Id,
                    AppNotificationNames.NewUserRegistered));

                _context.SaveChanges();
            }
        }
    }
}