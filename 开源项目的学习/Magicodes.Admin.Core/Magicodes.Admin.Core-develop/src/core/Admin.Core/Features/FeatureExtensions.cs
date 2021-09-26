// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : FeatureExtensions.cs
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

using Abp.Application.Features;
using Abp.Localization;

namespace Magicodes.Admin.Core.Features
{
    public static class FeatureExtensions
    {
        public static string GetValueText(this Feature feature, string value, ILocalizationContext localizationContext)
        {
            var featureMetadata = feature[FeatureMetadata.CustomFeatureKey] as FeatureMetadata;
            if (featureMetadata?.ValueTextNormalizer == null) return value;

            return featureMetadata.ValueTextNormalizer(value).Localize(localizationContext);
        }
    }
}