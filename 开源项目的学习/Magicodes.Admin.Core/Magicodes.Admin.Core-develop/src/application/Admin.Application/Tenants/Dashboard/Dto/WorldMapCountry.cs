// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : WorldMapCountry.cs
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

namespace Magicodes.Admin.Application.Tenants.Dashboard.Dto
{
    public class WorldMapCountry
    {
        public WorldMapCountry(string countryName, long color)
        {
            CountryName = countryName;
            Color = color;
        }

        public string CountryName { get; set; }
        public long Color { get; set; }
    }
}