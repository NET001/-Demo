// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : CurrencyConversionInput.cs
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
    public class CurrencyConversionInput
    {
        /// <summary>
        ///     区域
        /// </summary>
        [MaxLength(10)]
        [Required]
        public string CultureName { get; set; } //区域(例如：en-us)

        /// <summary>
        ///     金额
        /// </summary>
        [Required]
        public decimal CurrencyValue { get; set; }
    }
}