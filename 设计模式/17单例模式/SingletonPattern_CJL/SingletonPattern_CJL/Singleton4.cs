using System;
using System.Collections.Generic;
using System.Text;

namespace SingletonPattern_CJL
{
    /// <summary>
    /// 静态内部类懒汉模式同时线程安全
    /// </summary>
    public class Singleton4
    {
        private static class SingletonHolder
        {
            public static readonly Dictionary<string, Singleton4> instances = new Dictionary<string, Singleton4>()
            {
                ["key1"] = new Singleton4(),
                ["key2"] = new Singleton4(),
                ["key3"] = new Singleton4(),
            };
        }
        private Singleton4()
        {
            Console.WriteLine("创建了Singleton4实例");
        }
        public static Singleton4 GetInstance(string key)
        {
            return SingletonHolder.instances.TryGetValue(key, out _) ? SingletonHolder.instances[key] : null;
        }
    }
}
