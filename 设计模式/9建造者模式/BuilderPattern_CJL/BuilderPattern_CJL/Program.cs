using System;

namespace BuilderPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Product2 product2 = new Product2.Builder()
                 .SetParameter1("值1")
                 .SetParameter2("值2")
                 .SetParameter3("值3")
                 .Build();
            product2.Operation();
            Console.Read();
        }
        static void Demo0()
        {
            Director director = new Director();
            Builder b1 = new ConcreteBuilder1();
            Builder b2 = new ConcreteBuilder2();
            director.Construct(b1);
            Product0 p1 = b1.GetResult();
            p1.Show();
            director.Construct(b2);
            Product0 p2 = b2.GetResult();
            p2.Show();
        }
        static void Demo1()
        {
            Product1 p1 = new ConcreteIBuilder1()
                .SetPart1("值1")
                .SetPart2("值2")
                .SetPart3("值3")
                .Build();
            p1.Show();
            Product1 p2 = new ConcreteIBuilder2()
                .SetPart11("值11")
                .SetPart22("值22")
                .Build();
            p2.Show();
        }
        static void Demo2()
        {
            Product2 product2 = new Product2.Builder()
                 .SetParameter1("值1")
                 .SetParameter2("值2")
                 .SetParameter3("值3")
                 .Build();
            product2.Operation();
        }
    }
}