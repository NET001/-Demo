using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace Flyweight_CJL
{
    //享元类
    public class FlyweightClass
    {
        private string property1;
        public string Property1 => property1;
        private string property2;
        public string Property2 => property2;
        private string property3;
        public string Property3 => property3;
        public FlyweightClass(string property1, string property2, string property3)
        {
            this.property1 = property1;
            this.property2 = property2;
            this.property3 = property3;
        }

    }
    public enum FlyweightEnum
    {
        number1,
        number2,
        number3,
    }
    //享元工厂
    public class FlyweightClassFactory
    {
        ConcurrentDictionary<FlyweightEnum, FlyweightClass> keyValuePairs = new ConcurrentDictionary<FlyweightEnum, FlyweightClass>();
        public FlyweightClassFactory()
        {
            keyValuePairs[FlyweightEnum.number1] = new FlyweightClass("1", "1", "1");
            keyValuePairs[FlyweightEnum.number2] = new FlyweightClass("2", "2", "2");
            keyValuePairs[FlyweightEnum.number3] = new FlyweightClass("3", "3", "3");
        }
        //获取享元对象
        public FlyweightClass GetFlyweightClass(FlyweightEnum flyweightEnum)
        {
            return keyValuePairs[flyweightEnum];
        }
    }
    public class UnsharedConcreteFlyweightClass
    {
        FlyweightClass FlyweightClass;
        public string p1 { get; set; }
        public string p2 { get; set; }
        public UnsharedConcreteFlyweightClass(FlyweightClass FlyweightClass, string p1, string p2)
        {
            this.FlyweightClass = FlyweightClass;
            this.p1 = p1;
            this.p2 = p2;
        }
        public void Show()
        {
            Console.WriteLine(FlyweightClass.Property1 + "," + FlyweightClass.Property2 + "," + FlyweightClass.Property3 + "," + p1 + "," + p2);
        }
    }

}
