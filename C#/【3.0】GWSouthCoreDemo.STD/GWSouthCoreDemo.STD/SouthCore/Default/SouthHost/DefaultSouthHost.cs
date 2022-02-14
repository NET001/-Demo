using SouthCore.Core;
using SouthCore.Default.AddEquips;
using SouthCore.Default.Cache;
using SouthCore.Default.Log;
using SouthCore.Default.Synchronizer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SouthCore.Default
{
    public class DefaultSouthHost : ISouthHost
    {
        IDictionary<string, object> services;
        public IDictionary<string, object> Services => services;
        public DefaultSouthHost(IDictionary<string, object> services)
        {
            this.services = services;
        }
        public void Run()
        {
            //初始化缓存
            List<Type> caches = this.GetCaches();
            if (caches != null && caches.Count > 0)
            {
                Dictionary<string, object> dictCache = this.GetCacheStorage();
                foreach (var item in caches)
                {
                    dictCache.Add(item.Name, Activator.CreateInstance(item));
                }
            }
            //运行同步器
            this.GetSynchronizers().ForEach((t) =>
            {
                Task.Run(() =>
                {
                    //同步器
                    while (true)
                    {
                        try
                        {
                            t.Action?.Invoke(this);
                            Thread.Sleep(t.SyncSleep);
                        }
                        catch (Exception ex)
                        {
                            this.WriteDefaultLog(t.Name + "同步器异常", ex);
                            Thread.Sleep(t.RestartSleep);
                        }
                    }
                });
            });
        }
    }
}