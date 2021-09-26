// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : PositionEnum.cs
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Magicodes.Admin.Core.Contents
{
    /// <summary>
    ///     栏目位置
    /// </summary>
    public enum PositionEnum
    {
        /// <summary>
        ///     默认首页
        /// </summary>
        [Display(Name = "默认首页")] [Description("默认首页")]
        Default = 0
    }
}