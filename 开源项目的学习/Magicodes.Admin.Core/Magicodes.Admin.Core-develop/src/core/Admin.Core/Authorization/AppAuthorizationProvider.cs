// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppAuthorizationProvider.cs
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
using Abp.MultiTenancy;
using Magicodes.Admin.Localization;

namespace Magicodes.Admin.Core.Authorization
{
    /// <summary>
    ///     Application's authorization provider.
    ///     Defines permissions for the application.
    ///     See <see cref="AppPermissions" /> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ??
                        context.CreatePermission(AppPermissions.Pages, L("Pages"));
            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions,
                L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));

            var languages =
                administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create,
                L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete,
                L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts,
                L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits =
                administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits,
                    L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(
                AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree,
                L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers,
                L("ManagingMembers"));
            organizationUnits.CreateChildPermission(
                AppPermissions.Pages_Administration_OrganizationUnits_MemberBatchDelete, L("BatchDelete"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization,
                L("VisualSettings"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Pay_Settings, L("PaySettings"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_SmsCode_Settings,
                L("SmsCodeSettings"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Storage_Settings,
                L("StorageSettings"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_MiniProgram_Settings,
                L("MiniProgramSetting"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_WeChat_Settings,
                L("WeChatSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"),
                multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"),
                multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement,
                L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"),
                multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"),
                multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"),
                multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"),
                multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_BatchDelete, L("BatchDelete"),
                multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"),
                multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"),
                multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"),
                multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"),
                multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"),
                multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"),
                multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_BatchDelete, L("BatchDelete"),
                multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"),
                multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"),
                multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard,
                L("HangfireDashboard"),
                multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"),
                multiTenancySides: MultiTenancySides.Host);

            #region TransactionLog【交易日志】

            var transactionLog = pages.CreateChildPermission(AppPermissions.Pages_TransactionLog, L("TransactionLog"));
            //transactionLog.CreateChildPermission(AppPermissions.Pages_TransactionLog_Create, L("CreateNew"));
            transactionLog.CreateChildPermission(AppPermissions.Pages_TransactionLog_Edit, L("Edit"));
            transactionLog.CreateChildPermission(AppPermissions.Pages_TransactionLog_Delete, L("Delete"));
            transactionLog.CreateChildPermission(AppPermissions.Pages_TransactionLog_Restore, L("Restore"));

            #endregion

            #region ArticleInfo_ArticleTagInfos【文章标签】

            var articleInfo_articleTagInfos =
                pages.CreateChildPermission(AppPermissions.Pages_ArticleInfo_ArticleTagInfo, L("ArticleTagInfos"));
            articleInfo_articleTagInfos.CreateChildPermission(AppPermissions.Pages_ArticleInfo_ArticleTagInfo_Create,
                L("CreateNew"));
            articleInfo_articleTagInfos.CreateChildPermission(AppPermissions.Pages_ArticleInfo_ArticleTagInfo_Edit,
                L("Edit"));
            articleInfo_articleTagInfos.CreateChildPermission(AppPermissions.Pages_ArticleInfo_ArticleTagInfo_Delete,
                L("Delete"));
            articleInfo_articleTagInfos.CreateChildPermission(AppPermissions.Pages_ArticleInfo_ArticleTagInfo_Restore,
                L("Restore"));

            #endregion

            #region ArticleInfo【文章】

            var articleInfo = pages.CreateChildPermission(AppPermissions.Pages_ArticleInfo, L("ArticleInfo"));
            articleInfo.CreateChildPermission(AppPermissions.Pages_ArticleInfo_Create, L("CreateNew"));
            articleInfo.CreateChildPermission(AppPermissions.Pages_ArticleInfo_Edit, L("Edit"));
            articleInfo.CreateChildPermission(AppPermissions.Pages_ArticleInfo_Delete, L("Delete"));
            articleInfo.CreateChildPermission(AppPermissions.Pages_ArticleInfo_Restore, L("Restore"));

            #endregion

            #region ArticleSourceInfo【文章来源】

            var articleSourceInfo =
                pages.CreateChildPermission(AppPermissions.Pages_ArticleSourceInfo, L("ArticleSourceInfo"));
            articleSourceInfo.CreateChildPermission(AppPermissions.Pages_ArticleSourceInfo_Create, L("CreateNew"));
            articleSourceInfo.CreateChildPermission(AppPermissions.Pages_ArticleSourceInfo_Edit, L("Edit"));
            articleSourceInfo.CreateChildPermission(AppPermissions.Pages_ArticleSourceInfo_Delete, L("Delete"));
            articleSourceInfo.CreateChildPermission(AppPermissions.Pages_ArticleSourceInfo_Restore, L("Restore"));

            #endregion

            #region ColumnInfo【栏目】

            var columnInfo = pages.CreateChildPermission(AppPermissions.Pages_ColumnInfo, L("ColumnInfo"));
            columnInfo.CreateChildPermission(AppPermissions.Pages_ColumnInfo_Create, L("CreateNew"));
            columnInfo.CreateChildPermission(AppPermissions.Pages_ColumnInfo_Edit, L("Edit"));
            columnInfo.CreateChildPermission(AppPermissions.Pages_ColumnInfo_Delete, L("Delete"));
            columnInfo.CreateChildPermission(AppPermissions.Pages_ColumnInfo_Restore, L("Restore"));

            #endregion

            //SetCustomPermissions(pages);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LocalizationConsts.LocalizationSourceName);
        }
    }
}