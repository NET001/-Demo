using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TertiumQuid
{
    /// <summary>
    /// 中介者模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteMedium1 medium = new ConcreteMedium1();
            ConcreteWorker1 worker1 = new ConcreteWorker1(medium);
            ConcreteWorker2 worker2 = new ConcreteWorker2(medium);
            medium.ConcreteWorker1 = worker1;
            medium.ConcreteWorker2 = worker2;
            worker1.Send("你吃饭没？");
            worker2.Send("没有呢，一起去吗？");
            Console.ReadKey();
        }
    }
}
