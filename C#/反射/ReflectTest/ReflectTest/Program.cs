using ReflectObject;
using System;
using System.Reflection;

namespace ReflectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assembly
            Assembly assem = typeof(TestObject).Assembly;
            Console.WriteLine("程序集名称:" + assem.FullName);
            Console.WriteLine("名称:" + assem.GetName().Name);
            Console.WriteLine("版本:" + assem.GetName().Version);
            Console.WriteLine("程序集路径:" + assem.CodeBase);
            var t = assem.GetName();
            Console.WriteLine("Version:");
            //Module
            //ConstructorInfo
            //MethodInfo
            //FieldInfo
            //EventInfo
            //PropertyInfo
            //ParameterInfo
            //CustomAttributeData
            Console.ReadKey();
        }
    }
}
