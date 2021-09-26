// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppVersionHelper.cs
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
using System.IO;
using Abp.Reflection.Extensions;

namespace Magicodes.Admin.Core
{
    /// <summary>
    ///     Central point for application version.
    /// </summary>
    public class AppVersionHelper
    {
        /// <summary>
        ///     Gets current version of the application.
        ///     It's also shown in the web page.
        /// </summary>
        public const string Version = "6.1.0.0";

        /// <summary>
        ///     Gets release (last build) date of the application.
        ///     It's shown in the web page.
        /// </summary>
        public static DateTime ReleaseDate =>
            new FileInfo(typeof(AppVersionHelper).GetAssembly().Location).LastWriteTime;
    }
}