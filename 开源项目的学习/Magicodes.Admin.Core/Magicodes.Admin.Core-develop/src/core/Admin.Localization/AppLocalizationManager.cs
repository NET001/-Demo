// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppLocalizationManager.cs
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

using System.Globalization;
using Abp.Dependency;
using Abp.Localization;
using Abp.Localization.Sources;
using Magicodes.Admin.Core.Localization;

namespace Magicodes.Admin.Localization
{
    /// <summary>
    ///     APP本地化管理器
    /// </summary>
    public class AppLocalizationManager : IAppLocalizationManager, ITransientDependency
    {
        private ILocalizationSource _localizationSource;

        public AppLocalizationManager()
        {
            LocalizationManager = NullLocalizationManager.Instance;
        }

        public ILocalizationManager LocalizationManager { get; set; }

        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (_localizationSource == null || _localizationSource.Name != LocalizationConsts.AppLocalizationSourceName)
                    _localizationSource = LocalizationManager.GetSource(LocalizationConsts.LocalizationSourceName);

                return _localizationSource;
            }
        }

        /// <summary>
        ///     获取本地化字符串
        /// </summary>
        /// <param name="name">Key</param>
        /// <returns>本地化字符串</returns>
        public virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        ///     获取本地化字符串
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="args">参数</param>
        /// <returns>本地化字符串</returns>
        public string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        ///     获取本地化字符串
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="culture">语言</param>
        /// <returns>本地化字符串</returns>
        public string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        ///     获取本地化字符串
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="culture">语言</param>
        /// <param name="args"></param>
        /// <returns>本地化字符串</returns>
        public string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }
    }
}