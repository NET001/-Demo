using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

public class Program
{
    static void Main(string[] args)
    {
        List<string> list = new List<string>() { "1", "2" };
        list.ForEach(x => Console.WriteLine(x));
        Parallel.ForEach(list, x => Console.WriteLine(x));
        using (Obj1 obj1 = new Obj1())
        {
        }
        Console.ReadKey();
    }
    class Obj1 : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("执行了Dispose");
        }
    }
}