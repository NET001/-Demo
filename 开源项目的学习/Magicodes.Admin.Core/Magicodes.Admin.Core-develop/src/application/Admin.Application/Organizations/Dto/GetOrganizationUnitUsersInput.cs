// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : GetOrganizationUnitUsersInput.cs
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
using Abp.Runtime.Validation;
using Magicodes.Admin.Application.Core.Dto;

namespace Magicodes.Admin.Application.Organizations.Dto
{
    public class GetOrganizationUnitUsersInput : PagedAndSortedInputDto, IShouldNormalize
    {
        [Range(1, long.MaxValue)] public long Id { get; set; }

        public string FilterText { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
                Sorting = "user.Name, user.Surname";
            else if (Sorting.Contains("userName"))
                Sorting = Sorting.Replace("userName", "user.userName");
            else if (Sorting.Contains("addedTime")) Sorting = Sorting.Replace("addedTime", "uou.creationTime");
        }
    }
}