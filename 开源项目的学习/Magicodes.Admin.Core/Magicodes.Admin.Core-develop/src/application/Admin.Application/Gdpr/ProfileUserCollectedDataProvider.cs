// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ProfileUserCollectedDataProvider.cs
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
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.AspNetZeroCore.Net;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Localization;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Core;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.MultiTenancy;
using Magicodes.Admin.Core.Storage;
using Magicodes.Admin.Localization;

namespace Magicodes.Admin.Application.Gdpr
{
    public class ProfileUserCollectedDataProvider : IUserCollectedDataProvider, ITransientDependency
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly TenantManager _tenantManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserManager _userManager;

        public ProfileUserCollectedDataProvider(
            UserManager userManager,
            TenantManager tenantManager,
            ITempFileCacheManager tempFileCacheManager,
            IUnitOfWorkManager unitOfWorkManager,
            ILocalizationManager localizationManager)
        {
            _userManager = userManager;
            _tempFileCacheManager = tempFileCacheManager;
            _unitOfWorkManager = unitOfWorkManager;
            _localizationManager = localizationManager;
            _tenantManager = tenantManager;
        }

        public async Task<List<FileDto>> GetFiles(UserIdentifier user)
        {
            var tenancyName = ".";
            if (user.TenantId.HasValue)
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    tenancyName = (await _tenantManager.GetByIdAsync(user.TenantId.Value)).TenancyName;
                }

            var profileInfo = await _userManager.GetUserByIdAsync(user.UserId);

            var content = new List<string>
            {
                L("TenancyName") + ": " + tenancyName,
                L("UserName") + ": " + profileInfo.UserName,
                L("Name") + ": " + profileInfo.Name,
                L("Surname") + ": " + profileInfo.Surname,
                L("EmailAddress") + ": " + profileInfo.EmailAddress,
                L("PhoneNumber") + ": " + profileInfo.PhoneNumber
            };

            var profileInfoBytes = Encoding.UTF8.GetBytes(string.Join("\n\r", content));

            var file = new FileDto("ProfileInfo.txt", MimeTypeNames.TextPlain);
            _tempFileCacheManager.SetFile(file.FileToken, profileInfoBytes);

            return new List<FileDto> {file};
        }

        private string L(string name)
        {
            return _localizationManager.GetString(LocalizationConsts.LocalizationSourceName, name);
        }
    }
}