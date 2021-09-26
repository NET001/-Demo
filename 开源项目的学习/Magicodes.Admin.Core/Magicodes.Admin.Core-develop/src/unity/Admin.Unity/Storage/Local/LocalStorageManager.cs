using System;
using System.IO;
using Abp.Extensions;
using Abp.UI;
using Magicodes.Admin.Core.Configuration;
using Magicodes.Storage.Core;
using Magicodes.Storage.Local.Core;
using Microsoft.AspNetCore.Hosting;

namespace Magicodes.Admin.Unity.Storage.Local
{
    public class LocalStorageManager : ILocalStorageManager
    {
        private readonly IAppConfigurationAccessor _appConfiguration;
        private readonly IHostingEnvironment _env;

        public LocalStorageManager(IAppConfigurationAccessor appConfiguration, IHostingEnvironment env)
        {
            _appConfiguration = appConfiguration;
            _env = env;
        }

        public IStorageProvider StorageProvider { get; set; }


        /// <summary>
        /// 根据配置初始化存储提供程序
        /// </summary>
        public void Initialize()
        {
            #region 配置存储程序
            var rootPath = _appConfiguration.Configuration["StorageProvider:LocalStorageProvider:RootPath"];
            Console.WriteLine(rootPath);
            if (rootPath.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("本地存储配置错误！");
            }
            if (!rootPath.Contains(":"))
            {
                rootPath = Path.Combine(_env.WebRootPath, rootPath);
            }
            if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);

            StorageProvider = new LocalStorageProvider(rootPath, _appConfiguration.Configuration["StorageProvider:LocalStorageProvider:RootUrl"]);
            #endregion
        }
    }
}
