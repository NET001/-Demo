using System;
using System.Linq;
using System.Reflection;

namespace PrototypePattern_CJL
{
    class ObjTest : ICloneable
    {
        public string Property1 { get; set; } = "Property1";
        public string Property2 { get; set; } = "Property2";
        public string[] Property3 { get; set; } = new string[] { "Property3" };
        public ObjTest_Sub Property4 { get; set; } = new ObjTest_Sub()
        {
            Property1 = "Property1",
            Property2 = "Property2",
        };
        public ObjTest(string str)
        {
        }
        public object Clone()
        {
            return Clone2();
        }
        //浅拷贝
        private object Clone1()
        {
            return this.MemberwiseClone();
        }
        private object Clone2()
        {
            ObjTest result = (ObjTest)this.MemberwiseClone();
            result.Property3 = (string[])result.Property3.Clone();
            result.Property4 = (ObjTest_Sub)result.Property4.Clone();
            return result;
        }
    }
    class ObjTest_Sub : ICloneable
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ObjTest obj1 = new ObjTest("");
            ObjTest obj2 = (ObjTest)obj1.Clone();
            Console.WriteLine(obj1.Equals(obj2));
            Console.WriteLine(obj1.Property4.Equals(obj2.Property4));
            obj1.Property3[0] = "123";
            Console.WriteLine(obj2.Property3[0]);
            obj1.Property4.Property1 = "123";
            Console.WriteLine(obj2.Property4.Property1);
            Console.Read();
        }
        static void Demo2()
        {

        }
        static void Demo1()
        {
            ConcretePrototype1 p1 = new ConcretePrototype1("I");
            ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
            Console.WriteLine("Cloned: {0}", c1.Id);

            ConcretePrototype2 p2 = new ConcretePrototype2("II");
            ConcretePrototype2 c2 = (ConcretePrototype2)p2.Clone();
            Console.WriteLine("Cloned: {0}", c2.Id);
        }
    }
}