using ReflectObject;
using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Dynamic;

namespace ReflectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo7();
            Console.ReadKey();
        }

        //加载程序集并获取一些程序集信息
        static void Demo1()
        {
            //获取当前类所在的程序集
            Assembly assem1 = typeof(TestObject1).Assembly;

            string path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            path = Path.GetDirectoryName(path) + "\\extend\\netcoreapp3.1\\ReflectObject.dll";
            //能同时加载依赖项
            Assembly assem2 = Assembly.LoadFrom(path);
            //不包含依赖项
            Assembly assem3 = Assembly.LoadFile(path);
            //获取当前所在执行的程序集
            Assembly assem4 = Assembly.GetExecutingAssembly();
            //获取当前执行方法的程序集
            Assembly assem5 = Assembly.GetCallingAssembly();
            //以名称获取
            Assembly assem6 = Assembly.Load("程序集名称");
            //通过Type获取
            Assembly assem7 = Assembly.GetAssembly(typeof(TestObject1));
        }
        //获取程序集名称
        static void Demo2()
        {
            Assembly assem1 = typeof(TestObject1).Assembly;
            Console.WriteLine("程序集名称:" + assem1.FullName);
            Console.WriteLine("名称:" + assem1.GetName().Name);
            Console.WriteLine("版本:" + assem1.GetName().Version);
            Console.WriteLine("程序集路径:" + assem1.CodeBase);
        }
        //获取Type对象
        static void Demo3()
        {
            TestObject1 testObject1 = new TestObject1(10) { Property1 = 10, Property2 = "11" };
            Type type1 = testObject1.GetType();
            Type type2 = typeof(TestObject1);
            Assembly assem1 = typeof(TestObject1).Assembly;
            Type type3 = assem1.GetType("命名空间.类名");
            //创建实例
            assem1.CreateInstance("命名空间.类名");

        }
        //获取Type中的信息
        static void Demo4()
        {
            TestObject1 testObject1 = new TestObject1(10) { Property1 = 10, Property2 = "11" };
            TestObject2 testObject2 = new TestObject2() { Property1 = 10, Property2 = "11" };
            Type type1 = testObject1.GetType();
            //获取类名
            Console.WriteLine(type1.Name);
            //获取所在的命名空间
            Console.WriteLine(type1.Namespace);
            //获取程序集
            Console.WriteLine(type1.Assembly);
            //获取public字段
            FieldInfo[] fieldInfos1 = type1.GetFields();
            //获取public字段
            PropertyInfo[] propertyInfos1 = type1.GetProperties();
            //获取公共方法
            MethodInfo[] methodInfos1 = type1.GetMethods();
            //判断testObject1是否是由testObject2所派生出来的类
            testObject1.GetType().IsSubclassOf(testObject2.GetType());

        }
        //Type的操作
        static void Demo5()
        {
            //判断是否实现接口
            Type type1 = new List<string>().GetType();
            //判断是否实现了IEnumerable接口区分大小写
            bool flag1 = type1.GetInterface("IEnumerable", true) != null;
            Console.WriteLine(flag1);
            //判断是否是是指定类的子类
            bool flag2 = type1.IsSubclassOf(typeof(Object));
            Console.WriteLine(flag2);
        }
        //通过反射实例化
        static void Demo6<T>()
        {
            Type type1 = Assembly.Load("dll名称").GetType("命名空间.类名");
            //实例化无参构造函数
            object obj1 = Activator.CreateInstance(type1, true);
            //实例化有参构造函数
            object obj2 = Activator.CreateInstance(type1, new object[] { 1, "2" });
            //实例化泛型
            var type2 = Assembly.Load("dll名称").GetType("命名空间.类名" + "`1").MakeGenericType(typeof(TestObject2));
            List<TestObject2> obj3 = (List<TestObject2>)Activator.CreateInstance(type2, true);
            //实例化一个泛型
            T obj4 = Activator.CreateInstance<T>();
        }
        //修改集合中的值
        static void Demo7()
        {
            List<Demo7Obj> demo7Objs = new List<Demo7Obj>() {
                new Demo7Obj(){
                    MyProperty1="11",
                    MyProperty2="22"
                },
                new Demo7Obj(){
                    MyProperty1="111",
                    MyProperty2="222"
                },
                new Demo7Obj(){
                    MyProperty1="1111",
                    MyProperty2="2222"
                },
            };
            Type type1 = demo7Objs.GetType();
            //判断是否为泛型(搜索实现接口区分大小写)
            Type type2 = type1.GetInterface("IEnumerable", true);
            if (type2 != null)
            {
                IEnumerable<object> list = demo7Objs as IEnumerable<object>;
                //显示值
                foreach (var item in list)
                {
                    Type type3 = item.GetType();
                    foreach (var subItem in type3.GetProperties())
                    {
                        Console.WriteLine(subItem.Name);
                        Console.WriteLine(subItem.GetValue(item));
                    }
                }
                //设置值
                foreach (var item in list)
                {
                    Type type3 = item.GetType();
                    foreach (var subItem in type3.GetProperties())
                    {
                        subItem.SetValue(item, "123");
                    }
                }
                Console.WriteLine("已设置");
                //显示值
                foreach (var item in demo7Objs)
                {
                    Console.WriteLine(item.MyProperty1 + item.MyProperty2);
                }
            }


        }
        //反射执行方法
        static void Demo8()
        {
            TestObject1 testObject1 = new TestObject1(11);
            Type type = testObject1.GetType();
            Action action = (Action)Delegate.CreateDelegate(type, testObject1, "Func2");
            action();
        }
        /// <summary>
        /// 动态对象
        /// </summary>
        static void Demo9()
        {
            dynamic expObj = new ExpandoObject();
            expObj.FirstName = "Daffy";
            expObj.LastName = "Duck";
            Console.WriteLine(expObj.FirstName + " " + expObj.LastName);
            Func<DateTime, string> GetTomorrow = today => today.AddDays(1).ToShortDateString();
            expObj.GetTomorrowDate = GetTomorrow;
            Console.WriteLine("Tomorrow is {0}", expObj.GetTomorrowDate(DateTime.Now));
            expObj.Friends = new List<string>();
            expObj.Friends.Add("11");
            expObj.Friends.Add("22");
            expObj.Friends.Add("33");
            foreach (string friend in expObj.Friends)
            {
                Console.WriteLine(friend);
            }
        }
        class Demo7Obj
        {
            public string MyProperty1 { get; set; }
            public string MyProperty2 { get; set; }
        }
    }
}
