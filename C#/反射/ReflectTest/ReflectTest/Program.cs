using ReflectObject;
using System;
using System.Reflection;
using System.IO;

namespace ReflectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assembly程序集是以dll为单位,获取的是当前的dll
            //获取程序集的方式
            //通过类获取当前类所属的程序集
            Assembly assem1 = typeof(TestObject1).Assembly;
            //通过路径加载dll程序集
            string path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            path = Path.GetDirectoryName(path) + "\\extend\\netcoreapp3.1\\ReflectObject.dll";
            //能同时加载依赖项
            Assembly assem2 = Assembly.LoadFrom(path);
            //不包含依赖项
            Assembly assem3 = Assembly.LoadFile(path);
            //获取当前所在执行的程序集
            Assembly assem4 = Assembly.GetExecutingAssembly();
            //---------------------------------------------
            //获取程序集

            Console.WriteLine("程序集名称:" + assem1.FullName);
            Console.WriteLine("名称:" + assem1.GetName().Name);
            Console.WriteLine("版本:" + assem1.GetName().Version);
            Console.WriteLine("程序集路径:" + assem1.CodeBase);

            //Module
            var modules = assem1.Modules;


            //ConstructorInfo（发现类构造函数的属性，并提供对构造函数元素的访问权限）

            Type myType = typeof(TestObject1);
            Type[] types = new Type[1];
            types[0] = typeof(int);
            // Get the public instance constructor that takes an integer parameter.
            ConstructorInfo constructorInfoObj = myType.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public, null,
                CallingConventions.HasThis, types, null);
            Console.WriteLine(constructorInfoObj.ToString());


            //MethodInfo(发现方法的属性并提供对方法元数据的访问)
            MethodInfo[] methodInfos = typeof(TestObject1).GetMethods();
            //FieldInfo
            //EventInfo
            //PropertyInfo
            //ParameterInfo
            //CustomAttributeData
            Console.ReadKey();
        }
    }
}
