// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : GetEntityChangeInput.cs
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

using System;
using Abp.Runtime.Validation;
using Abp.Extensions;
using Magicodes.Admin.Application.Core.Dto;

namespace Magicodes.Admin.Application.Auditing.Dto
{
    public class GetEntityChangeInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserName { get; set; }

        public string EntityTypeFullName { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace()) Sorting = "ChangeTime DESC";

            if (Sorting.IndexOf("UserName", StringComparison.OrdinalIgnoreCase) >= 0)
                Sorting = "User." + Sorting;
            else
                Sorting = "EntityChange." + Sorting;
        }
    }
}