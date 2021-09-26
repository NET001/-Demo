// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ProfileControllerBase.cs
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

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Magicodes.Admin.Application;
using Magicodes.Admin.Application.Authorization.Users.Profile.Dto;
using Magicodes.Admin.Application.Core;
using Magicodes.Admin.Application.Core.Dto;
using Magicodes.Admin.Core.Storage;
using Magicodes.Admin.Web.Core.Helpers;

namespace Magicodes.Admin.Web.Host.Controllers
{
    public abstract class ProfileControllerBase : AdminControllerBase
    {
        private const int MaxProfilePictureSize = 5242880; //5MB

        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        protected ProfileControllerBase(ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
        }

        public UploadProfilePictureOutput UploadProfilePicture(FileDto input)
        {
            try
            {
                var profilePictureFile = Request.Form.Files.First();

                //Check input
                if (profilePictureFile == null) throw new UserFriendlyException(L("ProfilePicture_Change_Error"));

                if (profilePictureFile.Length > MaxProfilePictureSize)
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit",
                        AppConsts.MaxProfilPictureBytesUserFriendlyValue));

                byte[] fileBytes;
                using (var stream = profilePictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes)
                    .IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                    throw new Exception(L("IncorrectImageFormat"));

                _tempFileCacheManager.SetFile(input.FileToken, fileBytes);

                using (var bmpImage = new Bitmap(new MemoryStream(fileBytes)))
                {
                    return new UploadProfilePictureOutput
                    {
                        FileToken = input.FileToken,
                        FileName = input.FileName,
                        FileType = input.FileType,
                        Width = bmpImage.Width,
                        Height = bmpImage.Height
                    };
                }
            }
            catch (UserFriendlyException ex)
            {
                return new UploadProfilePictureOutput(new ErrorInfo(ex.Message));
            }
        }

        /// <summary>
        ///     上传头像返回图片Id
        /// </summary>
        public async Task<UploadProfilePictureOutputDto> UploadProfilePictureReturnFileId()
        {
            try
            {
                var profilePictureFile = Request.Form.Files.First();

                //Check input
                if (profilePictureFile == null) throw new UserFriendlyException(L("ProfilePicture_Change_Error"));

                if (profilePictureFile.Length > MaxProfilePictureSize)
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit", 5));

                byte[] fileBytes;
                using (var stream = profilePictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes)
                    .IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                    throw new UserFriendlyException(L("ProfilePicture_Change_Info", 5));

                var storedFile = new BinaryObject(AbpSession.TenantId, fileBytes.ToArray());
                await _binaryObjectManager.SaveAsync(storedFile);

                return new UploadProfilePictureOutputDto
                {
                    FileName = profilePictureFile.FileName,
                    ProfilePictureId = storedFile.Id
                };
            }
            catch (UserFriendlyException ex)
            {
                return new UploadProfilePictureOutputDto(new ErrorInfo(ex.Message));
            }
        }
    }
}