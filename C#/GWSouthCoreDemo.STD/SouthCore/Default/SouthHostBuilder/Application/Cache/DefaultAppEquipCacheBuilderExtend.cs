
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SouthCore.Default.Cache
{
    public static class DefaultAppEquipCacheBuilderExtend
    {
        #region Builder扩展
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetCaches(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipCacheBuilder> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            configure(new DefaultAppEquipCacheBuilder(builder));
            return builder;
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IDefaultAppEquipCacheBuilder SetCache<T>(this IDefaultAppEquipCacheBuilder builder)
        {
            builder.Builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton(new CacheDescriptor()
                {
                    Name = typeof(T),
                    Datas = new List<T>()
                });
            });
            return builder;

        }
        #endregion

        #region IServiceProvider扩展
        /// <summary>
        /// 获取缓存配置
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        internal static IEnumerable<CacheDescriptor> GetCaches(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetServices<CacheDescriptor>();
        }
        /// <summary>
        /// 获取缓存实例集合
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        internal static IEnumerable<CacheDescriptor> GetCacheStorages(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetServices<CacheDescriptor>();
        }
        /// <summary>
        /// 设置缓存实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="southHost"></param>
        /// <param name="datas"></param>
        internal static void SetCacheStorage<T>(this IServiceProvider serviceProvider, T datas) where T : IList
        {
            IEnumerable<CacheDescriptor> dict = serviceProvider.GetCacheStorages();
            foreach (CacheDescriptor cache in dict)
            {
                if (cache.Name == typeof(T))
                {
                    cache.Datas = datas;
                    break;
                }
            }
        }
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="southHost"></param>
        /// <returns></returns>
        public static List<T> GetCacheStorage<T>(this IServiceProvider serviceProvider)
        {
            IEnumerable<CacheDescriptor> dict = serviceProvider.GetCacheStorages();
            foreach (CacheDescriptor cache in dict)
            {
                if (cache.Name == typeof(T))
                {
                    return cache.Datas as List<T>;
                }
            }
            return null;
        }
        #endregion
    }
    public class CacheDescriptor
    {
        public Type Name { get; set; }
        public IList Datas { get; set; }
    }
}