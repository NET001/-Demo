using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SingletonPattern_CJL
{
    /// <summary>
    /// 饿汉模式
    /// </summary>
    public class Singleton1
    {
        private static readonly Singleton1 instance = new Singleton1();
        private Singleton1()
        {
            Console.WriteLine("创建了Singleton1实例");
        }
        public static Singleton1 GetInstance()
        {
            return instance;
        }
    }
}
