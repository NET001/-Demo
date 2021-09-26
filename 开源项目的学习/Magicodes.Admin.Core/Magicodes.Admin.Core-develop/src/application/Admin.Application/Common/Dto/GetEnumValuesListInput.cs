// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : GetEnumValuesListInput.cs
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

namespace Magicodes.Admin.Application.Common.Dto
{
    /// <summary>
    ///     获取枚举值列表
    /// </summary>
    public class GetEnumValuesListInput
    {
        /// <summary>
        ///     类型全名
        /// </summary>
        [Required]
        public string FullName { get; set; }
    }
}