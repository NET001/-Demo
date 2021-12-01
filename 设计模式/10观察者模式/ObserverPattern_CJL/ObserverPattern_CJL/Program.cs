using System;

namespace ObserverPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            EventBus eventBus = new EventBus();
            Concrete1Observer concrete11Observer = new Concrete1Observer();
            Concrete1Observer concrete12Observer = new Concrete1Observer();
            Concrete2Observer concrete2Observer = new Concrete2Observer();
            eventBus.Attach(concrete11Observer);
            eventBus.Attach(concrete12Observer);
            eventBus.Attach(concrete2Observer);
            eventBus.Notify(new Subject1()
            {
                c1 = "1111",
                c2 = "2222",
            });
            eventBus.Notify(new Subject2()
            {
                c3 = "3333",
                c4 = "4444",
            });
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
            EventBus eventBus = new EventBus();
            Concrete1Observer concrete1Observer = new Concrete1Observer();
            Concrete2Observer concrete2Observer = new Concrete2Observer();
            eventBus.Attach(concrete1Observer);
            eventBus.Attach(concrete2Observer);
            eventBus.Notify(new Subject1()
            {
                c1 = "1111",
                c2 = "2222",
            });
            eventBus.Notify(new Subject2()
            {
                c3 = "3333",
                c4 = "4444",
            });
        }
    }
}
