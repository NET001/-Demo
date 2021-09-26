// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AdminCoreModule.cs
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

using System;
using Abp.AspNetZeroCore;
using Abp.AspNetZeroCore.Timing;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MailKit;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Web.Models;
using Abp.Zero;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Magicodes.Admin.Core.Authorization.Roles;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.Configuration;
using Magicodes.Admin.Core.Debugging;
using Magicodes.Admin.Core.Emailing;
using Magicodes.Admin.Core.ErrorInfoConverter;
using Magicodes.Admin.Core.Features;
using Magicodes.Admin.Core.Friendships.Cache;
using Magicodes.Admin.Core.MultiTenancy;
using Magicodes.Admin.Core.MultiTenancy.Payments.Cache;
using Magicodes.Admin.Core.Notifications;
using Magicodes.Admin.Localization;

namespace Magicodes.Admin.Core
{
    [DependsOn(
        typeof(LocalizationModule),
        typeof(AbpZeroCoreModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetZeroCoreModule),
        typeof(AbpMailKitModule))]
    public class AdminCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            //workaround for issue: https://github.com/aspnet/EntityFrameworkCore/issues/9825
            //related github issue: https://github.com/aspnet/EntityFrameworkCore/issues/10407
            AppContext.SetSwitch("Microsoft.EntityFrameworkCore.Issue9825", true);

            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Adding feature providers
            Configuration.Features.Providers.Add<AppFeatureProvider>();

            //Adding setting providers

            Configuration.Settings.Providers.Add<AppSettingProvider>();

            //Adding notification providers
            Configuration.Notifications.Providers.Add<AppNotificationProvider>();

            //Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = AdminConsts.MultiTenancyEnabled;

            //Enable LDAP authentication (It can be enabled only if MultiTenancy is disabled!)
            //Configuration.Modules.ZeroLdap().Enable(typeof(AppLdapAuthenticationSource));

            //Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            if (DebugHelper.IsDebug)
                //Disabling email sending in debug mode
                Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);

            Configuration.ReplaceService(typeof(IEmailSenderConfiguration), () =>
            {
                Configuration.IocManager.IocContainer.Register(
                    Component.For<IEmailSenderConfiguration, ISmtpEmailSenderConfiguration>()
                        .ImplementedBy<AdminSmtpEmailSenderConfiguration>()
                        .LifestyleTransient()
                );
            });

            Configuration.Caching.Configure(FriendCacheItem.CacheName,
                cache => { cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30); });

            Configuration.Caching.Configure(PaymentCacheItem.CacheName,
                cache =>
                {
                    cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(AdminConsts.PaymentCacheDurationInMinutes);
                });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminCoreModule).GetAssembly());

            //注册自定义的异常转换器
            IocManager.RegisterIfNot<IExceptionToErrorInfoConverter, AdminErrorInfoConverter>(DependencyLifeStyle
                .Singleton);
        }

        public override void PostInitialize()
        {
            //修改默认的错误转换器
            IocManager.Resolve<IErrorInfoBuilder>().AddExceptionConverter(IocManager.Resolve<IExceptionToErrorInfoConverter>());
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}