// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UploadProfilePictureOutputDto.cs
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
using Abp.Web.Models;

namespace Magicodes.Admin.Application.Authorization.Users.Profile.Dto
{
    public class UploadProfilePictureOutputDto : ErrorInfo
    {
        public UploadProfilePictureOutputDto()
        {
        }

        public UploadProfilePictureOutputDto(ErrorInfo error)
        {
            Code = error.Code;
            Details = error.Details;
            Message = error.Message;
            ValidationErrors = error.ValidationErrors;
        }

        /// <summary>
        /// </summary>
        public Guid? ProfilePictureId { get; set; }

        public string FileName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}