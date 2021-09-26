// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppSettings.cs
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

namespace Magicodes.Admin.Core.Configuration
{
    /// <summary>
    ///     Defines string constants for setting names in the application.
    ///     See <see cref="AppSettingProvider" /> for setting definitions.
    /// </summary>
    public static class AppSettings
    {
        public static class HostManagement
        {
            public const string BillingLegalName = "App.HostManagement.BillingLegalName";
            public const string BillingAddress = "App.HostManagement.BillingAddress";
            public const string BillingTaxNumber = "App.HostManagement.BillingTaxNumber";
            public const string BillingContact = "App.HostManagement.BillingContact";
            public const string BillingBankAccount = "App.HostManagement.BillingBankAccount";
            public const string BillingBank = "App.HostManagement.BillingBank";
        }

        public static class UiManagement
        {
            public const string LayoutType = "App.UiManagement.LayoutType";
            public const string ContentSkin = "App.UiManagement.ContentSkin";
            public const string Theme = "App.UiManagement.Theme";

            public static class Header
            {
                public const string DesktopFixedHeader = "App.UiManagement.Header.DesktopFixedHeader";
                public const string DesktopMinimizeMode = "App.UiManagement.Header.DesktopMinimizeMode";
                public const string MobileFixedHeader = "App.UiManagement.Header.MobileFixedHeader";
                public const string Skin = "App.UiManagement.Header.Skin";
                public const string DisplaySubmenuArrowDesktop = "App.UiManagement.Header.DisplaySubmenuArrow_Desktop";
            }

            public static class LeftAside
            {
                public const string Position = "App.UiManagement.Left.Position";
                public const string AsideSkin = "App.UiManagement.Left.AsideSkin";
                public const string FixedAside = "App.UiManagement.Left.FixedAside";
                public const string AllowAsideMinimizing = "App.UiManagement.Left.AllowAsideMinimizing";
                public const string DefaultMinimizedAside = "App.UiManagement.Left.DefaultMinimizedAside";
                public const string AllowAsideHiding = "App.UiManagement.Left.AllowAsideHiding";
                public const string DefaultHiddenAside = "App.UiManagement.Left.DefaultHiddenAside";
                public const string SubmenuToggle = "App.UiManagement.Left.SubmenuToggle";
                public const string DropdownSubmenuSkin = "App.UiManagement.Left.DropdownSubmenuSkin";
                public const string DropdownSubmenuArrow = "App.UiManagement.Left.DropdownSubmenuArrow";
            }

            public static class Footer
            {
                public const string FixedFooter = "App.UiManagement.Footer.FixedFooter";
            }
        }


        public static class TenantManagement
        {
            public const string AllowSelfRegistration = "App.TenantManagement.AllowSelfRegistration";

            public const string IsNewRegisteredTenantActiveByDefault =
                "App.TenantManagement.IsNewRegisteredTenantActiveByDefault";

            public const string UseCaptchaOnRegistration = "App.TenantManagement.UseCaptchaOnRegistration";
            public const string DefaultEdition = "App.TenantManagement.DefaultEdition";

            public const string SubscriptionExpireNotifyDayCount =
                "App.TenantManagement.SubscriptionExpireNotifyDayCount";

            public const string BillingLegalName = "App.HostManagement.BillingLegalName";
            public const string BillingAddress = "App.HostManagement.BillingAddress";
            public const string BillingTaxNumber = "App.HostManagement.BillingTaxNumber";
            public const string BillingContact = "App.HostManagement.BillingContact";
            public const string BillingBankAccount = "App.HostManagement.BillingBankAccount";
            public const string BillingBank = "App.HostManagement.BillingBank";
        }

        public static class UserManagement
        {
            public const string AllowSelfRegistration = "App.UserManagement.AllowSelfRegistration";

            public const string IsNewRegisteredUserActiveByDefault =
                "App.UserManagement.IsNewRegisteredUserActiveByDefault";

            public const string UseCaptchaOnRegistration = "App.UserManagement.UseCaptchaOnRegistration";
            public const string SmsVerificationEnabled = "App.UserManagement.SmsVerificationEnabled";

            public static class TwoFactorLogin
            {
                public const string IsGoogleAuthenticatorEnabled =
                    "App.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled";
            }
        }

        public static class CacheKeys
        {
            public const string TenantRegistrationCache = "TenantRegistrationCache";
        }
    }
}