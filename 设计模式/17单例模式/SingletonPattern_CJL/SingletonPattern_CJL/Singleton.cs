using System;
using System.Collections.Generic;
using System.Text;

namespace SingletonPattern_CJL
{
    /// <summary>
    /// 单例类
    /// </summary>
    public class Singleton
    {
        private static Singleton instance;
        private static readonly object syncRoot = new object();
        private Singleton()
        {
        }
        public static Singleton GetInstance()
        {
            if (instance == null)
            {

                lock (syncRoot)
                {

                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
}
