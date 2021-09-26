// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AdminEntityFrameworkCoreModule.cs
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
using System.Collections.Generic;
using System.Reflection;
using Abp;
using Abp.Dapper;
using Abp.Dependency;
using Abp.Domain.Entities.Auditing;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Extensions;
using Abp.IdentityServer4;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Magicodes.Admin.Core;
using Magicodes.Admin.Core.Configuration;
using Magicodes.Admin.Core.EntityHistory;
using Magicodes.Admin.EntityFrameworkCore.Migrations.Seed;

namespace Magicodes.Admin.EntityFrameworkCore.EntityFramework
{
    [DependsOn(
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(AbpDapperModule),
        typeof(AdminCoreModule),
        typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule)
    )]
    public class AdminEntityFrameworkCoreModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            var isUseRowNumber = true;
            var isEnableEntityHistory = false;
            //判断是否注册（单元测试时无法在此时注册）
            if (IocManager.IsRegistered<IAppConfigurationAccessor>())
                using (var configurationAccessorObj = IocManager.ResolveAsDisposable<IAppConfigurationAccessor>())
                {
                    //从配置文件获取是否使用RowNumber进行分页
                    isUseRowNumber =
                        Convert.ToBoolean(configurationAccessorObj.Object.Configuration["Database:IsUseRowNumber"] ??
                                          "true");
                    //是否启用数据实体历史记录
                    isEnableEntityHistory =
                        Convert.ToBoolean(configurationAccessorObj.Object.Configuration["Abp:IsEnableEntityHistory"] ??
                                          "false");
                }


            if (!SkipDbContextRegistration)
                Configuration.Modules.AbpEfCore().AddDbContext<AdminDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                        AdminDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    else
                        AdminDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString,
                            isUseRowNumber);
                });

            if (isEnableEntityHistory)
            {
                //启用实体历史
                Configuration.EntityHistory.Selectors.Add(
                    new NamedTypeSelector(
                        "FullAuditedEntities",
                        type => typeof(IFullAudited).IsAssignableFrom(type)
                    )
                );
                Configuration.EntityHistory.Selectors.Add("AdminEntities", EntityHistoryHelper.TrackedTypes);
                Configuration.CustomConfigProviders.Add(new EntityHistoryConfigProvider(Configuration));
            }
            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminEntityFrameworkCoreModule).GetAssembly());
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly>
                {typeof(AdminEntityFrameworkCoreModule).GetAssembly()});
        }

        public override void PostInitialize()
        {
            using (var scope = IocManager.CreateScope())
            {
                var configurationAccessor = IocManager.Resolve<IAppConfigurationAccessor>();
                if (!SkipDbSeed && scope.Resolve<DatabaseCheckHelper>()
                        .Exist(configurationAccessor.Configuration["ConnectionStrings:Default"]))
                {
                    //系统启动时自动执行迁移
                    if (Convert.ToBoolean(configurationAccessor.Configuration["Database:AutoMigrate"] ?? "true") &&
                        !configurationAccessor.Configuration["ConnectionStrings:Default"].IsNullOrEmpty())
                        scope.Resolve<MultiTenantMigrateExecuter>().Run();
                    SeedHelper.SeedHostDb(IocManager);
                }
            }
        }
    }
}