// ======================================================================
// 
//           Copyright (C) 2019-2030 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : PagedAndFilteredInputDto.cs
//           description :
// 
//           created by 雪雁 at  2019-10-10 18:12
//           文档官网：https://docs.xin-lai.com
//           公众号教程：麦扣聊技术
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
// 
// ======================================================================

using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Magicodes.Admin.Application.Core.Dto
{
    public class PagedAndFilteredInputDto : IPagedResultRequest
    {
        public PagedAndFilteredInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }

        public string Filter { get; set; }

        [Range(1, AppConsts.MaxPageSize)] public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)] public int SkipCount { get; set; }
    }
}