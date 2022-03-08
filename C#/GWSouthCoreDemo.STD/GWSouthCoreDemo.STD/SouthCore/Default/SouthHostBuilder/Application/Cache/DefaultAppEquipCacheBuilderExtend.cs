using SouthCore.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SouthCore.Default.Cache
{
    public static class DefaultAppEquipCacheBuilderExtend
    {
        #region 服务名称
        private const string CacheCollections = "CacheCollections";
        internal const string CacheStorageCollections = "CacheStorageCollections";
        #endregion

        #region Builder扩展
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetCaches(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipCacheBuilder> configure)
        {
            IDefaultAppEquipCacheBuilder appbuilder = new DefaultAppEquipCacheBuilder();
            configure(appbuilder);
            foreach (var key in appbuilder.Services.Keys)
            {
                builder.Services[key] = appbuilder.Services[key];
            }
            builder.Services.AddServices(CacheStorageCollections, new Dictionary<string, object>());
            return builder;
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IDefaultAppEquipCacheBuilder SetCache<T>(this IDefaultAppEquipCacheBuilder builder) where T : IList
        {
            builder.Services.AddServices(CacheCollections, typeof(T));
            return builder;
        }

        #endregion

        #region Host扩展
        /// <summary>
        /// 获取缓存配置
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        internal static List<Type> GetCaches(this ISouthHost southHost)
        {
            List<Type> result = southHost.Services.GetServices<Type>(CacheCollections);
            return result;
        }
        /// <summary>
        /// 获取缓存实例集合
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> GetCacheStorage(this ISouthHost southHost)
        {
            return southHost.Services.GetServices<Dictionary<string, object>>(CacheStorageCollections).FirstOrDefault();
        }
        /// <summary>
        /// 设置缓存实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="southHost"></param>
        /// <param name="datas"></param>
        internal static void SetCacheStorage<T>(this ISouthHost southHost, T datas) where T : IList
        {
            Dictionary<string, object> dict = southHost.GetCacheStorage();
            string name = typeof(T).Name;
            foreach (var key in dict.Keys)
            {
                if (key == name)
                {
                    dict[key] = datas;
                    break;
                }
            }
        }
        #endregion

    }
}