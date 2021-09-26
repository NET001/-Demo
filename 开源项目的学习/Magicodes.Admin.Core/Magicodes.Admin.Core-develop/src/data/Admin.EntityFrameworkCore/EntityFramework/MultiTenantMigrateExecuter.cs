// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : MultiTenantMigrateExecuter.cs
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
using Abp.Data;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Castle.Core.Logging;
using Magicodes.Admin.Core.MultiTenancy;
using Magicodes.Admin.EntityFrameworkCore.Migrations.Seed;

namespace Magicodes.Admin.EntityFrameworkCore.EntityFramework
{
    /// <summary>
    ///     数据库迁移
    /// </summary>
    public class MultiTenantMigrateExecuter : ITransientDependency
    {
        private readonly IDbPerTenantConnectionStringResolver _connectionStringResolver;
        private readonly AbpZeroDbMigrator _migrator;
        private readonly IRepository<Tenant> _tenantRepository;

        public MultiTenantMigrateExecuter(
            AbpZeroDbMigrator migrator,
            IRepository<Tenant> tenantRepository,
            ILogger logger,
            IDbPerTenantConnectionStringResolver connectionStringResolver)
        {
            Logger = logger;
            _migrator = migrator;
            _tenantRepository = tenantRepository;
            _connectionStringResolver = connectionStringResolver;
        }

        public ILogger Logger { get; }

        /// <summary>
        ///     执行迁移
        /// </summary>
        public void Run()
        {
            var hostConnStr =
                _connectionStringResolver.GetNameOrConnectionString(
                    new ConnectionStringResolveArgs(MultiTenancySides.Host));
            if (hostConnStr.IsNullOrWhiteSpace())
            {
                Logger.Error("没有找到名称为'Default'的连接字符串!");
                return;
            }

            Logger.Info("--------------------------------------------------------");
            Logger.Info("主数据库: " + ConnectionStringHelper.GetConnectionString(hostConnStr));
            Logger.Info("主数据库自动迁移已启动...");

            try
            {
                _migrator.CreateOrMigrateForHost(SeedHelper.SeedHostDb);
            }
            catch (Exception ex)
            {
                Logger.Info("迁移主数据库时发生错误:");
                Logger.Error(ex.Message, ex);
                Logger.Info("迁移已取消!");
                return;
            }

            Logger.Info("主数据库迁移完成.");
            Logger.Info("--------------------------------------------------------");

            var migratedDatabases = new HashSet<string>();
            var tenants = _tenantRepository.GetAllList(t => t.ConnectionString != null && t.ConnectionString != "");
            for (var i = 0; i < tenants.Count; i++)
            {
                var tenant = tenants[i];
                Logger.Info(string.Format("租户数据库迁移已启动... ({0} / {1})", i + 1, tenants.Count));
                Logger.Info("名称 ： " + tenant.Name);
                Logger.Info("租户名称 ： " + tenant.TenancyName);
                Logger.Info("租户Id ： " + tenant.Id);
                Logger.Info("连接字符串 ： " + SimpleStringCipher.Instance.Decrypt(tenant.ConnectionString));

                if (!migratedDatabases.Contains(tenant.ConnectionString))
                {
                    try
                    {
                        _migrator.CreateOrMigrateForTenant(tenant);
                    }
                    catch (Exception ex)
                    {
                        Logger.Info("迁移租户数据库时发生错误:");
                        Logger.Info(ex.Message, ex);
                        Logger.Info("已跳过当前迁移继续下一个租户数据库迁移...");
                    }

                    migratedDatabases.Add(tenant.ConnectionString);
                }
                else
                {
                    Logger.Info("当前数据库已迁移. 已跳过....");
                }

                Logger.Info(string.Format("租户数据库迁移完成. ({0} / {1})", i + 1, tenants.Count));
                Logger.Info("--------------------------------------------------------");
            }

            Logger.Info("所有数据库均已完成迁移.");
        }
    }
}