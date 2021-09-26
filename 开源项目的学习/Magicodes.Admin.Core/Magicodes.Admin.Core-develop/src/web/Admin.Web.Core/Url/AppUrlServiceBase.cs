// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppUrlServiceBase.cs
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

using Abp.Dependency;
using Abp.Extensions;
using Abp.MultiTenancy;
using Magicodes.Admin.Application.Url;
using Magicodes.Admin.Core.Url;

namespace Magicodes.Admin.Web.Core.Url
{
    public abstract class AppUrlServiceBase : IAppUrlService, ITransientDependency
    {
        protected readonly ITenantCache TenantCache;

        protected readonly IWebUrlService WebUrlService;

        protected AppUrlServiceBase(IWebUrlService webUrlService, ITenantCache tenantCache)
        {
            WebUrlService = webUrlService;
            TenantCache = tenantCache;
        }

        public abstract string EmailActivationRoute { get; }

        public abstract string PasswordResetRoute { get; }

        public string CreateEmailActivationUrlFormat(int? tenantId)
        {
            return CreateEmailActivationUrlFormat(GetTenancyName(tenantId));
        }

        public string CreatePasswordResetUrlFormat(int? tenantId)
        {
            return CreatePasswordResetUrlFormat(GetTenancyName(tenantId));
        }

        public string CreateEmailActivationUrlFormat(string tenancyName)
        {
            var activationLink = WebUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/') +
                                 EmailActivationRoute + "?userId={userId}&confirmationCode={confirmationCode}";

            if (tenancyName != null) activationLink = activationLink + "&tenantId={tenantId}";

            return activationLink;
        }

        public string CreatePasswordResetUrlFormat(string tenancyName)
        {
            var resetLink = WebUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/') + PasswordResetRoute +
                            "?userId={userId}&resetCode={resetCode}";

            if (tenancyName != null) resetLink = resetLink + "&tenantId={tenantId}";

            return resetLink;
        }


        private string GetTenancyName(int? tenantId)
        {
            return tenantId.HasValue ? TenantCache.Get(tenantId.Value).TenancyName : null;
        }
    }
}