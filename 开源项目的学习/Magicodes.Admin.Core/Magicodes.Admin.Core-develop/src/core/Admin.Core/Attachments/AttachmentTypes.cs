// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AttachmentTypes.cs
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
    ///     附件类型
    /// </summary>
    public enum AttachmentTypes
    {
        /// <summary>
        ///     普通文件
        /// </summary>
        File = 0,

        /// <summary>
        ///     图片
        /// </summary>
        Image = 1,

        /// <summary>
        ///     视频
        /// </summary>
        Video = 2,

        /// <summary>
        ///     音频
        /// </summary>
        Audio = 3
    }
}