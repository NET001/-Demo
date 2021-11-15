using System;
using System.Collections.Generic;
using System.Text;

namespace SingletonPattern_CJL
{
    /// <summary>
    ///懒汉模式加锁加双重检验
    /// </summary>
    public class Singleton2
    {
        private static Singleton2 instance;
        private static readonly object syncRoot = new object();
        private Singleton2()
        {
            Console.WriteLine("创建了Singleton2实例");
        }
        public static Singleton2 GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new Singleton2();
                    }
                }
            }
            return instance;
        }
    }
}
