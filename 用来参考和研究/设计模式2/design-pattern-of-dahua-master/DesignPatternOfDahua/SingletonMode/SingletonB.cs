using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonMode
{
    /// <summary>
    /// 饿汉模式
    /// </summary>
    public class SingletonB
    {
        public static readonly SingletonB singleton = new SingletonB();

        private SingletonB() { }

        public static SingletonB CreateInstance()
        {
            return singleton;
        }
    }
}
