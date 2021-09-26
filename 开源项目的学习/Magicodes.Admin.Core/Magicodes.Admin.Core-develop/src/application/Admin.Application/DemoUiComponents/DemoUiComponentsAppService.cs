// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : DemoUiComponentsAppService.cs
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
using System.Collections.Generic;
using System.Linq;
using Abp;
using Abp.Authorization;
using Magicodes.Admin.Application.Core;
using Magicodes.Admin.Application.DemoUiComponents.Dto;
using Magicodes.Admin.Core.Authorization;

namespace Magicodes.Admin.Application.DemoUiComponents
{
    [AbpAuthorize(AppPermissions.Pages_DemoUiComponents)]
    public class DemoUiComponentsAppService : AdminAppServiceBase, IDemoUiComponentsAppService
    {
        public List<NameValue<string>> GetCountries(string searchTerm)
        {
            var countries = new List<NameValue<string>>
            {
                new NameValue {Name = "Turkey", Value = "1"},
                new NameValue {Name = "United States of America", Value = "2"},
                new NameValue {Name = "Russian Federation", Value = "3"},
                new NameValue {Name = "France", Value = "4"},
                new NameValue {Name = "Spain", Value = "5"},
                new NameValue {Name = "Germany", Value = "6"},
                new NameValue {Name = "Netherlands", Value = "7"},
                new NameValue {Name = "China", Value = "8"},
                new NameValue {Name = "Italy", Value = "9"},
                new NameValue {Name = "Switzerland", Value = "10"},
                new NameValue {Name = "South Africa", Value = "11"},
                new NameValue {Name = "Belgium", Value = "12"},
                new NameValue {Name = "Brazil", Value = "13"},
                new NameValue {Name = "India", Value = "14"}
            };

            return countries.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        public List<NameValue<string>> SendAndGetSelectedCountries(List<NameValue<string>> selectedCountries)
        {
            return selectedCountries;
        }

        public StringOutput SendAndGetValue(string input)
        {
            return new StringOutput
            {
                Output = input
            };
        }

        #region date & time pickers

        public DateToStringOutput SendAndGetDate(DateTime? date)
        {
            return new DateToStringOutput
            {
                DateString = date?.ToString("d")
            };
        }

        public DateToStringOutput SendAndGetDateTime(DateTime? date)
        {
            return new DateToStringOutput
            {
                DateString = date?.ToString("g")
            };
        }

        public DateToStringOutput SendAndGetDateRange(DateTime? startDate, DateTime? endDate)
        {
            return new DateToStringOutput
            {
                DateString = startDate?.ToString("d") + " - " + endDate?.ToString("d")
            };
        }

        #endregion
    }
}