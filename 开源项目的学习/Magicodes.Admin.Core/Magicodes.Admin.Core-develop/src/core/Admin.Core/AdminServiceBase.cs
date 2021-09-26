// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AdminServiceBase.cs
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

using Abp;
using Magicodes.Admin.Localization;

namespace Magicodes.Admin.Core
{
    /// <summary>
    ///     This class can be used as a base class for services in this application.
    ///     It has some useful objects property-injected and has some basic methods most of services may need to.
    ///     It's suitable for non domain nor application service classes.
    ///     For domain services inherit <see cref="AdminDomainServiceBase" />.
    ///     For application services inherit AdminAppServiceBase.
    /// </summary>
    public abstract class AdminServiceBase : AbpServiceBase
    {
        protected AdminServiceBase()
        {
            LocalizationSourceName = LocalizationConsts.LocalizationSourceName;
        }
    }
}