﻿// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : RecommendedTypes.cs
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
    ///     推荐类型
    /// </summary>
    public enum RecommendedTypes
    {
        /// <summary>
        ///     置顶
        /// </summary>
        [Display(Name = "置顶")] Top = 0,

        /// <summary>
        ///     热门
        /// </summary>
        [Display(Name = "热门")] Hot = 1,

        /// <summary>
        ///     推荐
        /// </summary>
        [Display(Name = "推荐")] Recommend = 2,

        /// <summary>
        ///     普通
        /// </summary>
        [Display(Name = "普通")] Common = 3
    }
}