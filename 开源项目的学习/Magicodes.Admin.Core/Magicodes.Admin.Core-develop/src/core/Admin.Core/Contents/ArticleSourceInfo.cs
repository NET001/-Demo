// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : ArticleSourceInfo.cs
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
    ///     文章来源（原创、转载）
    /// </summary>
    [Display(Name = "文章来源")]
    public class ArticleSourceInfo : EntityBase<long>
    {
        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}