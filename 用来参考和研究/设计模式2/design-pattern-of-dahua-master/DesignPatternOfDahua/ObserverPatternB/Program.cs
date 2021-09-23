using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ObserverPatternB.ConcreteNotifierA;

namespace ObserverPatternB
{
    /// <summary>
    /// 观察者模式（利用委托）
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteNotifierA concreteNotifierA = new ConcreteNotifierA();
            ConcreteObserverA concreteObserverA = new ConcreteObserverA("小张", concreteNotifierA);
            ConcreteObserverB concreteObserverB= new ConcreteObserverB("小李", concreteNotifierA);
            concreteNotifierA.NotifyState = "老板回来了";
            concreteNotifierA.update += concreteObserverA.UpdateConcreteObserverB;
            concreteNotifierA.update += concreteObserverB.UpdateConcreteObserverB;
            concreteNotifierA.Notify();
            Console.ReadKey();
        }
    }
}
