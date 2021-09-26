using Abp;
using Abp.Dependency;
using Magicodes.Storage.Core;

namespace Magicodes.Admin.Unity.Storage.Default
{
    /// <summary>
    /// 存储管理程序
    /// </summary>
    public interface IStorageManager: ISingletonDependency, IShouldInitialize
    {
        /// <summary>
        /// 存储提供程序
        /// </summary>
        IStorageProvider StorageProvider { get; set; }
    }
}