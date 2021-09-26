// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ColumnTypes.cs
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

using System.ComponentModel.DataAnnotations;

namespace Magicodes.Admin.Core.Contents
{
    /// <summary>
    ///     栏目类型
    /// </summary>
    public enum ColumnTypes
    {
        /// <summary>
        ///     Html文本
        /// </summary>
        [Display(Name = "Html文本")] Html = 0,

        /// <summary>
        ///     图片
        /// </summary>
        [Display(Name = "图片")] Image = 1
    }
}