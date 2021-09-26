// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : GetLanguageTextsInput.cs
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
using Abp.Application.Services.Dto;
using Abp.Localization;
using Abp.Runtime.Validation;
using Abp.Extensions;

namespace Magicodes.Admin.Application.Localization.Dto
{
    public class GetLanguageTextsInput : IPagedResultRequest, ISortedResultRequest, IShouldNormalize
    {
        [Required]
        [MaxLength(ApplicationLanguageText.MaxSourceNameLength)]
        public string SourceName { get; set; }

        [StringLength(ApplicationLanguage.MaxNameLength)]
        public string BaseLanguageName { get; set; }

        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength, MinimumLength = 2)]
        public string TargetLanguageName { get; set; }

        public string TargetValueFilter { get; set; }

        public string FilterText { get; set; }

        [Range(0, int.MaxValue)] public int MaxResultCount { get; set; } //0: Unlimited.

        [Range(0, int.MaxValue)] public int SkipCount { get; set; }

        public void Normalize()
        {
            if (TargetValueFilter.IsNullOrEmpty()) TargetValueFilter = "ALL";
        }

        public string Sorting { get; set; }
    }
}