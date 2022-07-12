using System;
using System.Threading.Tasks;
using System.Threading;

namespace 引用类型测试
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Demo4();
            Console.ReadLine();
        }
        private static void Demo1(List<string> list)
        {
            list.Add("1");
        }
        private static void Demo2(ref string str)
        {
            str = "123";
        }
        private static void Demo3()
        {
            List<string> list = new List<string>();
            string str = "";
            Demo1(list);
            Demo2(ref str);
        }
        private static void Demo4()
        { 
            IEnumerable<string> list=new List<string>();
            list.ToList().Add("1");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
