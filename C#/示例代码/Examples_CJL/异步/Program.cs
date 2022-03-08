using System;
using System.Threading.Tasks;
using System.Threading;

namespace 异步
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Obj1();
            Console.ReadLine();
        }
        static void Demo1()
        {
            Task.Run(() =>
            {
                int i = 0;
                while (true)
                {
                    Console.WriteLine(i++);
                    Thread.Sleep(100);
                }
            });
        }
        static void Demo2()
        {
            Obj2 obj2 = new Obj2();
            obj2.event1 += delegate ()
            {
                int i = 0;
                while (true)
                {
                    Console.WriteLine(i++);
                    Thread.Sleep(100);
                }
            };
            obj2.StartEvent1();
        }
    }
    class Obj1
    {
        public Obj1()
        {
            for (int i = 0; i < 3; i++)
            {
                InitVoid();
            }
        }
        public async Task InitVoid()
        {
            string result = await InitString();
            Console.WriteLine(result);
        }
        public async Task<string> InitString()
        {
            await Demo1();
            Demo2();
            return "完成了Init执行";
        }
        static async Task<string> Demo1()
        {
            Console.WriteLine("进入了Demo1");
            string result = await Task.Run<string>(() => { Thread.Sleep(2000); return ""; });
            Console.WriteLine("完成了Demo1执行");
            return result;
        }
        static void Demo2()
        {
            Console.WriteLine("进入了Demo2");
            Thread.Sleep(2000);
            Console.WriteLine("完成了Demo2执行");
        }
    }
    class Obj2
    {
        public event Action event1;
        public void StartEvent1()
        {
            event1.Invoke();
        }
    }
}
