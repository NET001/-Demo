// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : FileDto.cs
//           description :
// 
//           created by 雪雁 at  2019-10-10 18:12
//           文档官网：https://docs.xin-lai.com
//           公众号教程：麦扣聊技术
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
// 
// ======================================================================

using System;
using System.ComponentModel.DataAnnotations;

namespace Magicodes.Admin.Application.Core.Dto
{
    public class FileDto
    {
        public FileDto()
        {
        }

        public FileDto(string fileName, string fileType)
        {
            FileName = fileName;
            FileType = fileType;
            FileToken = Guid.NewGuid().ToString("N");
        }

        [Required] public string FileName { get; set; }

        public string FileType { get; set; }

        [Required] public string FileToken { get; set; }
    }
}