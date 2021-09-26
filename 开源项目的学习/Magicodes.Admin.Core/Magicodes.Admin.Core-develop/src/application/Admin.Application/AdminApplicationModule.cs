// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AdminApplicationModule.cs
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

using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Magicodes.Admin.Application.AutoMapper;
using Magicodes.Admin.Core;
using Magicodes.Admin.Core.Authorization;

namespace Magicodes.Admin.Application
{
    /// <summary>
    ///     Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(AdminCoreModule)
    )]
    public class AdminApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(AdminDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AdminApplicationModule).GetAssembly());
        }
    }
}