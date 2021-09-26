// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : IImagesDto.cs
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

namespace Magicodes.Admin.Core.Attachments
{
    /// <summary>
    ///     图片Dto定义
    /// </summary>
    public interface IImagesDto
    {
        /// <summary>
        ///     文件大小
        /// </summary>
        long FileLength { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     链接
        /// </summary>
        string Url { get; set; }
    }
}