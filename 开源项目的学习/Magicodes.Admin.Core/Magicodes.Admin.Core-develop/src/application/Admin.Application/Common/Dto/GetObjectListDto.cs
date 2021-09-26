// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : GetObjectImagesListDto.cs
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

using Abp.AutoMapper;
using Magicodes.Admin.Core.Attachments;

namespace Magicodes.Admin.Application.Common.Dto
{
    /// <summary>
    ///     图片显示Dto
    /// </summary>
    [AutoMapFrom(typeof(AttachmentInfo))]
    public class GetObjectListDto : IImagesDto
    {
        public long Id { get; set; }

        /// <summary>
        ///     是否封面
        /// </summary>
        public bool IsCover { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     文件大小
        /// </summary>
        public long FileLength { get; set; }

        /// <summary>
        ///     网络路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// BlobName
        /// </summary>
        public string BlobName { get; set; }
    }
}