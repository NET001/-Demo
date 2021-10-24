using System;

namespace CompositePattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteAggregate a = new ConcreteAggregate();
            a[0] = "数据a";
            a[1] = "数据b";
            a[2] = "数据c";
            a[3] = "数据d";
            a[4] = "数据e";
            a[5] = "数据f";
            Iterator i = new ConcreteIterator(a);
            object item = i.First();
            while (!i.IsDone())
            {
                Console.WriteLine(i.CurrentItem());
                i.Next();
            }
            Iterator id = new ConcreteIteratorDesc(a);
            object itemd = i.First();
            while (!id.IsDone())
            {
                Console.WriteLine(id.CurrentItem());
                id.Next();
            }
            Console.Read();
        }
    }
}