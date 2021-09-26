using Abp;
using Abp.Dependency;
using Magicodes.Storage.Core;

namespace Magicodes.Admin.Unity.Storage.Local
{
    public interface ILocalStorageManager : ISingletonDependency, IShouldInitialize
    {
        /// <summary>
        /// 存储提供程序
        /// </summary>
        IStorageProvider StorageProvider { get; set; }
    }
}
