// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : TestAppConfigurationAccessor.cs
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

using Abp.Dependency;
using Abp.Reflection.Extensions;
using Magicodes.Admin.Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace Magicodes.Admin.Tests.Base.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(AdminTestBaseModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public IConfigurationRoot Configuration { get; }
    }
}