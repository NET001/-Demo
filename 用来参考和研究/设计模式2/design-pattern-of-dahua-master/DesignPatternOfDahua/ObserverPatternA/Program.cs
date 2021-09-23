using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternA
{
    /// <summary>
    /// 观察者模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {  
            ConcreteNotifierA notifier1 = new ConcreteNotifierA();
            Observer observer1 = new ConcreteObserverA("小张", notifier1);
            Observer observer2 = new ConcreteObserverA("小李", notifier1);
            notifier1.Add(observer1);
            notifier1.Add(observer2);
            notifier1.Action = "老板回来了";
            notifier1.Notify();

            ConcreteNotifierB notifier2 = new ConcreteNotifierB();
            Observer observer3 = new ConcreteObserverB("小赵", notifier2);
            Observer observer4 = new ConcreteObserverB("小王", notifier2);
            notifier2.Add(observer3);
            notifier2.Add(observer4);
            notifier2.Action = "老板回来啦";
            notifier2.Notify();

            Console.ReadKey();
        }
    }
}
