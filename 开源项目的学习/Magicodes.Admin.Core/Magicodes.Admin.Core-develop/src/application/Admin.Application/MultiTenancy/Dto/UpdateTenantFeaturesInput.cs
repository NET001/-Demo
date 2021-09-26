// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : UpdateTenantFeaturesInput.cs
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
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Magicodes.Admin.Application.MultiTenancy.Dto
{
    public class UpdateTenantFeaturesInput
    {
        [Range(1, int.MaxValue)] public int Id { get; set; }

        [Required] public List<NameValueDto> FeatureValues { get; set; }
    }
}