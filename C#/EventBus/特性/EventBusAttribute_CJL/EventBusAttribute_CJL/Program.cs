using System;

namespace EventBusAttribute_CJL
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
    }
    public class Subject1
    {
        public string c1 { get; set; }
        public string c2 { get; set; }
    }
    public class Concrete1Observer
    {
        [EventBusFunction(typeof(Subject1))]
        public void Update(object obj)
        {
            Subject1 subject1 = obj as Subject1;
            Console.WriteLine("Concrete1Observer接收到数据" + subject1.c1 + "," + subject1.c2);
        }
    }
    public class Subject2
    {
        public string c3 { get; set; }
        public string c4 { get; set; }
    }
    public class Concrete2Observer
    {
        [EventBusFunction(typeof(Subject2))]
        public void Operation(object obj)
        {
            Subject2 subject2 = obj as Subject2;
            Console.WriteLine("Concrete2Observer接收到数据" + subject2.c3 + "," + subject2.c4);
        }
    }
}
