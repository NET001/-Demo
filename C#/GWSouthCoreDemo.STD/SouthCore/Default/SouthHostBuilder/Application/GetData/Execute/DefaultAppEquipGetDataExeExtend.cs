using SouthCore.Common;

using SouthCore.Default.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using SouthCore.Default.Http;
using SouthCore.Default.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace SouthCore.Default.GetData
{
    public static class DefaultAppEquipGetDataExeExtend
    {
        /// <summary>
        /// 调用HttpSend
        /// </summary>
        /// <param name="exe"></param>
        /// <param name="name"></param>
        /// <param name="paramet"></param>
        /// <returns></returns>
        public static object[] HttpSend(this IDefaultAppEquipGetDataExe exe, string name, object[] paramet)
        {
            object[] respon = exe.ServiceProvider.GetHttpSend(name, paramet);
            return respon;
        }
        /// <summary>
        /// 从Cache中获取Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="southHost"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static object GetCacheData<T>(this IDefaultAppEquipGetDataExe exe, Func<T, bool> func) where T : class
        {
            object obj = exe.ServiceProvider.GetServices<CacheDescriptor>()
                ?.Where(t => t.Name == typeof(T))
                .FirstOrDefault().Datas;
            List<T> cacheStorages = obj as List<T>;
            object result = cacheStorages?.Where(func).FirstOrDefault();
            return result;
        }
    }
}