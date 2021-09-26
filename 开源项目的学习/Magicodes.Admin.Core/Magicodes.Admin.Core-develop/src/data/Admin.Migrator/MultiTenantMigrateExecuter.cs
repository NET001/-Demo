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
using Magicodes.Admin.Core.MultiTenancy;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework;
using Magicodes.Admin.EntityFrameworkCore.Migrations.Seed;

namespace Magicodes.Admin.Migrator
{
    public class MultiTenantMigrateExecuter : ITransientDependency
    {
        private readonly IDbPerTenantConnectionStringResolver _connectionStringResolver;

        private readonly AbpZeroDbMigrator _migrator;
        private readonly IRepository<Tenant> _tenantRepository;

        public MultiTenantMigrateExecuter(
            AbpZeroDbMigrator migrator,
            IRepository<Tenant> tenantRepository,
            Log log,
            IDbPerTenantConnectionStringResolver connectionStringResolver)
        {
            Log = log;

            _migrator = migrator;
            _tenantRepository = tenantRepository;
            _connectionStringResolver = connectionStringResolver;
        }

        public Log Log { get; }

        public void Run(bool skipConnVerification)
        {
            var hostConnStr =
                _connectionStringResolver.GetNameOrConnectionString(
                    new ConnectionStringResolveArgs(MultiTenancySides.Host));
            if (hostConnStr.IsNullOrWhiteSpace())
            {
                Log.Write("请在配置文件配置默认的连接字符串“Default”！");
                return;
            }

            Log.Write("主数据库： " + ConnectionStringHelper.GetConnectionString(hostConnStr));
            if (!skipConnVerification)
            {
                Log.Write("即将迁移所有数据库，是否继续..? (Y/N): ");
                var command = Console.ReadLine();
                if (!command.IsIn("Y", "y"))
                {
                    Log.Write("迁移已取消。");
                    return;
                }
            }

            Log.Write("开始执行主数据库迁移...");

            try
            {
                _migrator.CreateOrMigrateForHost(SeedHelper.SeedHostDb);
            }
            catch (Exception ex)
            {
                Log.Write("An error occured during migration of host database:");
                Log.Write(ex.ToString());
                Log.Write("Canceled migrations.");
                return;
            }

            Log.Write("主数据库迁移完成。");
            Log.Write("--------------------------------------------------------");

            var migratedDatabases = new HashSet<string>();
            var tenants = _tenantRepository.GetAllList(t => t.ConnectionString != null && t.ConnectionString != "");
            for (var i = 0; i < tenants.Count; i++)
            {
                var tenant = tenants[i];
                Log.Write(string.Format("开始迁移租户数据库... ({0} / {1})", i + 1, tenants.Count));
                Log.Write("Name              : " + tenant.Name);
                Log.Write("TenancyName       : " + tenant.TenancyName);
                Log.Write("Tenant Id         : " + tenant.Id);
                Log.Write("Connection string : " + SimpleStringCipher.Instance.Decrypt(tenant.ConnectionString));

                if (!migratedDatabases.Contains(tenant.ConnectionString))
                {
                    try
                    {
                        _migrator.CreateOrMigrateForTenant(tenant);
                    }
                    catch (Exception ex)
                    {
                        Log.Write("An error occured during migration of tenant database:");
                        Log.Write(ex.ToString());
                        Log.Write("Skipped this tenant and will continue for others...");
                    }

                    migratedDatabases.Add(tenant.ConnectionString);
                }
                else
                {
                    Log.Write(
                        "This database has already migrated before (you have more than one tenant in same database). Skipping it....");
                }

                Log.Write(string.Format("租户数据库迁移完成. ({0} / {1})", i + 1, tenants.Count));
                Log.Write("--------------------------------------------------------");
            }

            Log.Write("所有数据库已完成迁移。");
        }
    }
}