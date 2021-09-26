// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : FeatureInputTypeDto.cs
//           description :
// 
//           created by 雪雁 at  2019-06-17 10:17
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using System.Collections.Generic;
using Abp.Runtime.Validation;

namespace Magicodes.Admin.Application.Editions.Dto
{
    //Mapped in CustomDtoMapper
    public class FeatureInputTypeDto
    {
        public string Name { get; set; }

        public IDictionary<string, object> Attributes { get; set; }

        public IValueValidator Validator { get; set; }

        public LocalizableComboboxItemSourceDto ItemSource { get; set; }
    }
}