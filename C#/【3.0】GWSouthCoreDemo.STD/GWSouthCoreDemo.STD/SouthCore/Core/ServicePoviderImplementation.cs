using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Core
{
    /// <summary>
    /// 服务容器的实现
    /// </summary>
    public static class ServicePoviderImplementation
    {
        public static void AddServices<T>(this IDictionary<string, object> dict, string name, T data) where T : class
        {

            if (dict.TryGetValue(name, out _))
            {
                (dict[name] as List<T>)?.Add(data);
            }
            else
            {
                dict[name] = new List<T>() { data };
            }
        }
        public static List<T> GetServices<T>(this IDictionary<string, object> dict, string name) where T : class
        {
            return dict[name] as List<T>;
        }
    }
}
