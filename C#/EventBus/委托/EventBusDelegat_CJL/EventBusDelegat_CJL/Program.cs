using System;

namespace EventBusDelegat_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            var action = new Action<Subject1>((Subject1 sub1) =>
            {
                Console.WriteLine(sub1.c1 + "," + sub1.c2);
            });
            EventBus<Subject1>.Subscriber(action);
            //EventBus<Subject1>.UnSubscriber(action);
            EventBus<Subject1>.Subscriber((Subject1 sub1) =>
            {
                Console.WriteLine(sub1.c1 + "/" + sub1.c2);
            });
            EventBus<Subject2>.Subscriber((Subject2 sub2) =>
            {
                Console.WriteLine(sub2.c3 + "," + sub2.c4);
            });
            try
            {
                EventBus<Subject1>.PublishAsync(new Subject1()
                {
                    c1 = "111",
                    c2 = "222"
                });
                EventBus<Subject2>.PublishAsync(new Subject2()
                {
                    c3 = "333",
                    c4 = "444"
                });
            }
            catch (Exception ex)
            {

            }
            Console.ReadKey();
        }
    }

    public class Subject1
    {
        public string c1 { get; set; }
        public string c2 { get; set; }
    }
    public class Subject2
    {
        public string c3 { get; set; }
        public string c4 { get; set; }
    }
}
