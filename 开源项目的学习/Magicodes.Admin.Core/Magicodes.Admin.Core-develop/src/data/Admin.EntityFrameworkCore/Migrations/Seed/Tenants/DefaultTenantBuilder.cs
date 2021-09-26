// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : DefaultTenantBuilder.cs
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
using Abp.MultiTenancy;
using Magicodes.Admin.Core.Editions;
using Magicodes.Admin.Core.MultiTenancy;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.EntityFrameworkCore.Migrations.Seed.Tenants
{
    public class DefaultTenantBuilder
    {
        private readonly AdminDbContext _context;

        public DefaultTenantBuilder(AdminDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
        }

        private void CreateDefaultTenant()
        {
            //Default tenant
            var defaultTenant = _context.Tenants.IgnoreQueryFilters()
                .FirstOrDefault(t => t.TenancyName == AbpTenantBase.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new Tenant(AbpTenantBase.DefaultTenantName, AbpTenantBase.DefaultTenantName);

                var defaultEdition = _context.Editions.IgnoreQueryFilters()
                    .FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null) defaultTenant.EditionId = defaultEdition.Id;

                _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }
        }
    }
}