// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : EntityHistoryConfigProvider.cs
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

using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Abp.Configuration.Startup;

namespace Magicodes.Admin.Core.EntityHistory
{
    public class EntityHistoryConfigProvider : ICustomConfigProvider
    {
        private readonly IAbpStartupConfiguration _abpStartupConfiguration;

        public EntityHistoryConfigProvider(IAbpStartupConfiguration abpStartupConfiguration)
        {
            _abpStartupConfiguration = abpStartupConfiguration;
        }

        public Dictionary<string, object> GetConfig(CustomConfigProviderContext customConfigProviderContext)
        {
            var entityHistoryConfig = new Dictionary<string, object>();

            if (!_abpStartupConfiguration.EntityHistory.IsEnabled) return entityHistoryConfig;

            foreach (var type in EntityHistoryHelper.TrackedTypes)
                if (_abpStartupConfiguration.EntityHistory.Selectors.Any(s => s.Predicate(type)))
                    entityHistoryConfig.Add(type.FullName ?? "", type.FullName);

            return entityHistoryConfig;
        }
    }
}