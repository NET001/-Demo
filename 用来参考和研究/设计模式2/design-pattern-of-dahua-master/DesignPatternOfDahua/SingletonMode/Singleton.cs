using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonMode
{
    /// <summary>
    /// 多线程 懒汉模式
    /// </summary>
    public class Singleton
    {
        public static Singleton singleton;
        public static readonly object _lock = new object();

        private Singleton() { }

        public static Singleton CreateInstance()
        {
            if (singleton == null)
            {
                lock (_lock)
                {
                    if (singleton == null)
                    {
                        singleton = new Singleton();
                    }
                }
            }
            return singleton;
        }
    }
}
