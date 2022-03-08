using SouthCore.Default.Cache;
using SouthCore.Default.Http;
using System;
using System.Collections;

namespace SouthCore.Default.Synchronizer
{
    public static class DefaultAppEquipSynchronizerExeExtend
    {
        public static void HttpMapCacheStorage<CacheT>(this IDefaultAppEquipSynchronizerExe exe, string httpName, Func<object[], CacheT> func, object[] parameter = null) where CacheT : IList
        {
            object[] response = exe.ServiceProvider.GetHttpSend(httpName, parameter);
            CacheT datas = func.Invoke(response);
            exe.ServiceProvider.SetCacheStorage(datas);
        }
        public static void SetCacheStorage<T>(this IDefaultAppEquipSynchronizerExe exe, T datas) where T : IList
        {
            exe.ServiceProvider.SetCacheStorage(datas);
        }
        public static object[] GetHttpSend(this IDefaultAppEquipSynchronizerExe exe, string name, object[] parameter = null)
        {
            return exe.ServiceProvider.GetHttpSend(name, parameter);
        }
    }
}