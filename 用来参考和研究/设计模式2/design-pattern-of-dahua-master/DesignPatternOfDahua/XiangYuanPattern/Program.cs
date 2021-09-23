using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangYuanPattern
{
    /// <summary>
    /// 享元模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int fly = 22;
            FlyweightFactory flyweightFactory = new FlyweightFactory();
            Flyweight fx = flyweightFactory.CreateFlyweight("X");
            fx.Oparation(--fly);

            Flyweight fy = flyweightFactory.CreateFlyweight("Y");
            fy.Oparation(--fly);

            Flyweight fz = flyweightFactory.CreateFlyweight("Z");
            fz.Oparation(--fly);

            UnShareConcrateFlyweight unShare = new UnShareConcrateFlyweight();
            unShare.Oparation(--fly);

            Console.ReadKey();
        }
    }
}
