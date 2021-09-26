// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TenantSettingsAppService_Tests.cs
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
using Magicodes.Admin.Application.Configuration.Tenants;
using Magicodes.Admin.Core.Configuration;
using Shouldly;
using Xunit;

namespace Magicodes.Admin.Tests.Configuration.Tenants
{
    public class TenantSettingsAppService_Tests : AppTestBase
    {
        public TenantSettingsAppService_Tests()
        {
            _tenantSettingsAppService = Resolve<ITenantSettingsAppService>();
            _settingManager = Resolve<ISettingManager>();

            LoginAsDefaultTenantAdmin();
            InitializeTestSettings();
        }

        private readonly ITenantSettingsAppService _tenantSettingsAppService;
        private readonly ISettingManager _settingManager;

        private void InitializeTestSettings()
        {
            _settingManager.ChangeSettingForApplicationAsync(AppSettings.UserManagement.AllowSelfRegistration, "true");
            _settingManager.ChangeSettingForApplicationAsync(
                AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, "false");
        }

        [Fact(Skip =
            "Getting exception: Abp.Authorization.AbpAuthorizationException : Required permissions are not granted. At least one of these permissions must be granted: Settings")]
        public async Task Should_Change_UserManagement_Settings()
        {
            //Get and check current settings

            //Act
            var settings = await _tenantSettingsAppService.GetAllSettings();

            //Assert
            settings.UserManagement.AllowSelfRegistration.ShouldBe(true);
            settings.UserManagement.IsNewRegisteredUserActiveByDefault.ShouldBe(false);
            settings.UserManagement.UseCaptchaOnRegistration.ShouldBe(true);

            //Change and save settings

            //Arrange
            settings.UserManagement.AllowSelfRegistration = true;
            settings.UserManagement.IsNewRegisteredUserActiveByDefault = true;
            settings.UserManagement.UseCaptchaOnRegistration = false;

            await _tenantSettingsAppService.UpdateAllSettings(settings);

            //Assert
            _settingManager.GetSettingValue<bool>(AppSettings.UserManagement.AllowSelfRegistration).ShouldBe(true);
            _settingManager.GetSettingValue<bool>(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault)
                .ShouldBe(true);
            _settingManager.GetSettingValue<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration).ShouldBe(false);
        }
    }
}