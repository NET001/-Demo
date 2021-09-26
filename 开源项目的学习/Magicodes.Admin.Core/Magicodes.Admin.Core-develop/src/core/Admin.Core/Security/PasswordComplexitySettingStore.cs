// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : PasswordComplexitySettingStore.cs
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
using Abp.Configuration;
using Abp.Dependency;
using Abp.Zero.Configuration;

namespace Magicodes.Admin.Core.Security
{
    public class PasswordComplexitySettingStore : IPasswordComplexitySettingStore, ITransientDependency
    {
        private readonly ISettingManager _settingManager;

        public PasswordComplexitySettingStore(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task<PasswordComplexitySetting> GetSettingsAsync()
        {
            return new PasswordComplexitySetting
            {
                RequireDigit =
                    await _settingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement
                        .PasswordComplexity.RequireDigit),
                RequireLowercase =
                    await _settingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement
                        .PasswordComplexity.RequireLowercase),
                RequireNonAlphanumeric =
                    await _settingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement
                        .PasswordComplexity.RequireNonAlphanumeric),
                RequireUppercase =
                    await _settingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement
                        .PasswordComplexity.RequireUppercase),
                RequiredLength =
                    await _settingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement
                        .PasswordComplexity.RequiredLength)
            };
        }
    }
}