using SouthCore.Default.Cache;
using SouthCore.Default.Http;
using System;
using System.Collections;

namespace SouthCore.Default.Synchronizer
{
    public static class DefaultAppEquipSynchronizerExeExtend
    {
        public static void HttpMapCacheStorage<CacheT>(this IDefaultAppEquipSynchronizerExe exe, string httpName, Func<string, CacheT> func) where CacheT : IList
        {
            string response = exe.SouthHost.GetHttpSend(httpName);
            CacheT datas = func.Invoke(response);
            exe.SouthHost.SetCacheStorage<CacheT>(datas);
        }
        public static void SetCacheStorage<T>(this IDefaultAppEquipSynchronizerExe exe, T datas) where T : IList
        {
            exe.SouthHost.SetCacheStorage(datas);
        }
        public static string GetHttpSend(this IDefaultAppEquipSynchronizerExe exe, string name, string parameter = null)
        {
            return exe.SouthHost.GetHttpSend(name, parameter);
        }
    }
}