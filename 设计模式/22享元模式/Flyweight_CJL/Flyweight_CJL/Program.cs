using System;
using System.Collections.Generic;

namespace Flyweight_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
            Console.Read();
        }
        static void Demo1()
        {
            int extrinsicstate = 22;
            FlyweightFactory f = new FlyweightFactory();
            Flyweight fx = f.GetFlyweight("X");
            fx.Operation(--extrinsicstate);
            Flyweight fy = f.GetFlyweight("Y");
            fy.Operation(--extrinsicstate);
            Flyweight fz = f.GetFlyweight("Z");
            fz.Operation(--extrinsicstate);
            UnsharedConcreteFlyweight uf = new UnsharedConcreteFlyweight();
            uf.Operation(--extrinsicstate);
        }
        static void Demo2()
        {
            FlyweightClassFactory flyweightClassFactory = new FlyweightClassFactory();
            List<UnsharedConcreteFlyweightClass> list = new List<UnsharedConcreteFlyweightClass>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(new UnsharedConcreteFlyweightClass(flyweightClassFactory.GetFlyweightClass(FlyweightEnum.number1), i.ToString(), i.ToString()));
            }
            list[0].Show();
        }
    }
}