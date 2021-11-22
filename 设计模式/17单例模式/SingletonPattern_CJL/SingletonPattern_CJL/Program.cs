using System;
using System.Diagnostics;
using System.Threading;

namespace SingletonPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            Console.Read();
        }
        /// <summary>
        /// 饿汉模式
        /// </summary>
        static void Demo1()
        {
            Singleton1 s1 = Singleton1.GetInstance();
            Singleton1 s2 = Singleton1.GetInstance();
            if (s1 == s2)
            {
                Console.WriteLine("两个是同一个对象");
            }
        }
        //懒汉模式加锁加双重检测
        static void Demo2()
        {
            Singleton2 s1 = Singleton2.GetInstance();
            Singleton2 s2 = Singleton2.GetInstance();
            if (s1 == s2)
            {
                Console.WriteLine("两个是同一个对象");
            }
        }
        /// <summary>
        /// 静态内部类
        /// </summary>
        static void Demo3()
        {
            Singleton3 s1 = Singleton3.GetInstance();
            Singleton3 s2 = Singleton3.GetInstance();
            if (s1 == s2)
            {
                Console.WriteLine("两个是同一个对象");
            }
        }
        static void Demo4()
        {
            Singleton4 s1 = Singleton4.GetInstance("key1");
            Singleton4 s2 = Singleton4.GetInstance("key1");
            if (s1 == s2)
            {
                Console.WriteLine("两个是同一个对象");
            }
        }
    }
}