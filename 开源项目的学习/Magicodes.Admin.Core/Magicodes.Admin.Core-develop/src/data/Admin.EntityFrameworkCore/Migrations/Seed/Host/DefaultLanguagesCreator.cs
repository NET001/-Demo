// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : DefaultLanguagesCreator.cs
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
using System.Linq;
using Abp.Localization;
using Magicodes.Admin.Core;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.EntityFrameworkCore.Migrations.Seed.Host
{
    public class DefaultLanguagesCreator
    {
        private readonly AdminDbContext _context;

        public DefaultLanguagesCreator(AdminDbContext context)
        {
            _context = context;
        }

        public static List<ApplicationLanguage> InitialLanguages => GetInitialLanguages();

        private static List<ApplicationLanguage> GetInitialLanguages()
        {
            var tenantId = AdminConsts.MultiTenancyEnabled ? null : (int?) 1;
            return new List<ApplicationLanguage>
            {
                new ApplicationLanguage(tenantId, "en", "English", "famfamfam-flags us"),
                new ApplicationLanguage(tenantId, "zh-CN", "简体中文", "famfamfam-flags cn")
            };
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages) AddLanguageIfNotExists(language);
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.IgnoreQueryFilters()
                .Any(l => l.TenantId == language.TenantId && l.Name == language.Name)) return;

            _context.Languages.Add(language);

            _context.SaveChanges();
        }
    }
}