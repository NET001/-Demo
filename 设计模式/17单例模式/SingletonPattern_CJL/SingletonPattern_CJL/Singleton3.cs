using System;
using System.Collections.Generic;
using System.Text;

namespace SingletonPattern_CJL
{
    /// <summary>
    /// 静态内部类懒汉模式同时线程安全
    /// </summary>
    public class Singleton3
    {
        private static class SingletonHolder
        {
            public static readonly Singleton3 instance = new Singleton3();
        }
        private Singleton3()
        {
            Console.WriteLine("创建了Singleton3实例");
        }
        public static Singleton3 GetInstance()
        {
            return SingletonHolder.instance;
        }
    }
}
