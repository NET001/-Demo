using System;

namespace ObserverPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
            Console.Read();
        }
        /// <summary>
        /// 同步阻塞的方式
        /// </summary>
        static void Demo1()
        {
            ConcreteSubject s = new ConcreteSubject();
            s.Attach(new ConcreteObserver(s, "X"));
            s.Attach(new ConcreteObserver(s, "Y"));
            s.Attach(new ConcreteObserver(s, "Z"));
            s.SubjectState = "ABC";
            s.Notify();
        }
        /// <summary>
        /// 简单的非阻塞异步
        /// </summary>
        static void Demo2()
        {
            AsyncConcreteSubject s = new AsyncConcreteSubject();
            s.Attach(new AsyncConcreteObserver(s, "X"));
            s.Attach(new AsyncConcreteObserver(s, "Y"));
            s.Attach(new AsyncConcreteObserver(s, "Z"));
            s.SubjectState = "ABC";
            s.Notify();
        }
        /// <summary>
        /// EventBus
        /// </summary>
        static void Demo3()
        {

        }
    }
}
