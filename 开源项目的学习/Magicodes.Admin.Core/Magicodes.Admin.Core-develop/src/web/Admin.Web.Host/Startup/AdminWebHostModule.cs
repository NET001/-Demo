// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AdminWebHostModule.cs
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

using System.Collections.Generic;
using Abp.AspNetCore.Configuration;
using Abp.AspNetZeroCore;
using Abp.AspNetZeroCore.Web.Authentication.External;
using Abp.AspNetZeroCore.Web.Authentication.External.OpenIdConnect;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using Magicodes.Admin.Application;
using Magicodes.Admin.Application.Custom;
using Magicodes.Admin.Core.Chat;
using Magicodes.Admin.Core.Configuration;
using Magicodes.Admin.Core.Debugging;
using Magicodes.Admin.Core.Friendships;
using Magicodes.Admin.Core.Identity;
using Magicodes.Admin.Core.MultiTenancy;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework;
using Magicodes.Admin.Unity;
using Magicodes.Admin.Web.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Magicodes.Admin.Web.Host.Startup
{
    [DependsOn(
        typeof(AdminApplicationModule),
        typeof(AdminWebCoreModule),
        typeof(AdminCustomModule),
        typeof(UnityModule)
        )]
    public class AdminWebHostModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IHostingEnvironment _env;

        public AdminWebHostModule(
            IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat =
                _appConfiguration["App:ServerRootAddress"] ?? "http://localhost:22742/";


            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = DebugHelper.IsDebug;

            Configuration.Modules.AspNetZero().LicenseCode = _appConfiguration["AbpZeroLicenseCode"];

            //配置后台动态web api
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(AdminApplicationModule).GetAssembly()
                );

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(AdminCustomModule).GetAssembly(),
                    "cus"
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminWebHostModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            using (var scope = IocManager.CreateScope())
            {
                if (!scope.Resolve<DatabaseCheckHelper>().Exist(_appConfiguration["ConnectionStrings:Default"])) return;
            }

            if (IocManager.Resolve<IMultiTenancyConfig>().IsEnabled)
            {
                var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
                workManager.Add(IocManager.Resolve<SubscriptionExpirationCheckWorker>());
                workManager.Add(IocManager.Resolve<SubscriptionExpireEmailNotifierWorker>());
            }

            ConfigureExternalAuthProviders();

            IocManager.RegisterIfNot<ISmsSender, NullSmsSender>();
            IocManager.RegisterIfNot<IChatCommunicator, NullChatCommunicator>();

            //初始化聊天状态监视
            IocManager.Resolve<ChatUserStateWatcher>().Initialize();
        }

        private void ConfigureExternalAuthProviders()
        {
            var externalAuthConfiguration = IocManager.Resolve<ExternalAuthConfiguration>();

            if (bool.Parse(_appConfiguration["Authentication:OpenId:IsEnabled"] ?? "false"))
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        OpenIdConnectAuthProviderApi.Name,
                        _appConfiguration["Authentication:OpenId:ClientId"],
                        _appConfiguration["Authentication:OpenId:ClientSecret"],
                        typeof(OpenIdConnectAuthProviderApi),
                        new Dictionary<string, string>
                        {
                            {"Authority", _appConfiguration["Authentication:OpenId:Authority"]},
                            {"LoginUrl", _appConfiguration["Authentication:OpenId:LoginUrl"]}
                        }
                    )
                );
        }
    }
}