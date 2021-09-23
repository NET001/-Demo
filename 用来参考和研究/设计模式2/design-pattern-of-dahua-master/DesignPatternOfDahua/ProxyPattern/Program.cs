using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    /// <summary>
    /// 代理模式
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            ProxyPerson proxyPerson = new ProxyPerson();
            proxyPerson.SendChocolates();
            proxyPerson.SendFlowers();
            proxyPerson.WatchMovie();
            Console.ReadKey();
        }
    }
}
