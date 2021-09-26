// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ProfilePictureUserCollectedDataProvider.cs
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
using System.Threading.Tasks;
using Abp;
using Abp.AspNetZeroCore.Net;
using Abp.Dependency;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.Storage;

namespace Magicodes.Admin.Application.Gdpr
{
    public class ProfilePictureUserCollectedDataProvider : IUserCollectedDataProvider, ITransientDependency
    {
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly UserManager _userManager;

        public ProfilePictureUserCollectedDataProvider(
            UserManager userManager,
            IBinaryObjectManager binaryObjectManager,
            ITempFileCacheManager tempFileCacheManager
        )
        {
            _userManager = userManager;
            _binaryObjectManager = binaryObjectManager;
            _tempFileCacheManager = tempFileCacheManager;
        }

        public async Task<List<FileDto>> GetFiles(UserIdentifier user)
        {
            var profilePictureId = (await _userManager.GetUserByIdAsync(user.UserId)).ProfilePictureId;
            if (!profilePictureId.HasValue) return new List<FileDto>();

            var profilePicture = await _binaryObjectManager.GetOrNullAsync(profilePictureId.Value);
            if (profilePicture == null) return new List<FileDto>();

            var file = new FileDto("ProfilePicture.png", MimeTypeNames.ImagePng);
            _tempFileCacheManager.SetFile(file.FileToken, profilePicture.Bytes);

            return new List<FileDto> {file};
        }
    }
}