using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    class BasePerson : DoSomething
    {
        public void SendChocolates()
        {
            Console.WriteLine("送巧克力");
        }

        public void SendFlowers()
        {
            Console.WriteLine("送花");
        }

        public void WatchMovie()
        {
            Console.WriteLine("看电影");
        }
    }
}
