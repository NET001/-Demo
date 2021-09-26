// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppAuthorizationProvider.Custom.cs
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

using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Magicodes.Admin.Localization;

namespace Magicodes.Admin.Core.Custom.Authorization
{
    /// <summary>
    ///     Application's authorization provider.
    ///     Defines permissions for the application.
    ///     See <see cref="AppCustomPermissions" /> for all permission names.
    /// </summary>
    public class AppCustomAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppCustomAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppCustomAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //TODO：用户自定义
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LocalizationConsts.LocalizationSourceName);
        }
    }
}