// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : EditionPaymentType.cs
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

namespace Magicodes.Admin.Core.Editions
{
    public enum EditionPaymentType
    {
        /// <summary>
        ///     Payment on first tenant registration.
        /// </summary>
        NewRegistration = 0,

        /// <summary>
        ///     Purchasing by an existing tenant that currently using trial version of a paid edition.
        /// </summary>
        BuyNow = 1,

        /// <summary>
        ///     A tenant is upgrading it's edition (either from a free edition or from a low-price paid edition).
        /// </summary>
        Upgrade = 2,

        /// <summary>
        ///     A tenant is extending it's current edition (without changing the edition).
        /// </summary>
        Extend = 3
    }
}