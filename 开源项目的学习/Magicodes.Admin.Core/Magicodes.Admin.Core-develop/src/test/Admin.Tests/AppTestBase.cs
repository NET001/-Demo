// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AppTestBase.cs
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
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization.Users;
using Abp.EntityFrameworkCore.Extensions;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.TestBase;
using Magicodes.Admin.Core.Authorization.Roles;
using Magicodes.Admin.Core.Authorization.Users;
using Magicodes.Admin.Core.MultiTenancy;
using Magicodes.Admin.EntityFrameworkCore;
using Magicodes.Admin.EntityFrameworkCore.EntityFramework;
using Magicodes.Admin.Tests.Base;
using Microsoft.EntityFrameworkCore;

namespace Magicodes.Admin.Tests
{
    public class AppTestBase : AppTestBase<AdminTestModule>
    {

    }
}