// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : IAppLocalizationManager.cs
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

namespace Magicodes.Admin.Core.Localization
{
    /// <summary>
    ///     APP本地化管理器
    /// </summary>
    public interface IAppLocalizationManager
    {
        /// <summary>
        ///     获取本地化字符串
        /// </summary>
        /// <param name="name">Key</param>
        /// <returns>本地化字符串</returns>
        string L(string name);

        /// <summary>
        ///     获取本地化字符串
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="args">参数</param>
        /// <returns>本地化字符串</returns>
        string L(string name, params object[] args);

        /// <summary>
        ///     获取本地化字符串
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="culture">语言</param>
        /// <returns>本地化字符串</returns>
        string L(string name, CultureInfo culture);

        /// <summary>
        ///     获取本地化字符串
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="culture">语言</param>
        /// <param name="args"></param>
        /// <returns>本地化字符串</returns>
        string L(string name, CultureInfo culture, params object[] args);
    }
}