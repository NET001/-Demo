using System;

namespace _1._0_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClassAction();
            MyStructAction();
            MyInterfaceAction();
            MyEventAction();
        }
        /// <summary>
        /// 类实例化，类存储在堆中
        /// </summary>
        static void MyClassAction()
        {
            //类实例化
            MyClass myClass = new MyClass();
            myClass.p1 = "MyClass";
            Console.WriteLine(myClass.p1);
        }
        /// <summary>
        /// 结构体实例化,结构体存储在栈
        /// </summary>
        static void MyStructAction()
        {
            MyStatic myStatic = new MyStatic("MyStatic", "321");
            Console.WriteLine(myStatic.p1);
        }
        /// <summary>
        /// 类必须实现接口中的成员
        /// </summary>
        static void MyInterfaceAction()
        {
            MyInterfaceRealize myInterfaceRealize = new MyInterfaceRealize();
            myInterfaceRealize.p1 = "MyInterface";
            Console.WriteLine(myInterfaceRealize.p1);

        }
        /// <summary>
        /// 注册事件并调用
        /// </summary>
        static void MyEventAction()
        {
            object MyEventAction_Sub1(object t)
            {
                MyEvent myEvent1 = t as MyEvent;
                myEvent1.p1 = "MyEventAction_Sub1";
                Console.WriteLine(myEvent1.p1);
                return myEvent1;
            }

            object MyEventAction_Sub2(object t)
            {
                MyEvent myEvent1 = t as MyEvent;
                myEvent1.p1 = "MyEventAction_Sub2";
                Console.WriteLine(myEvent1.p1);
                return myEvent1;
            }
            //注册多个事件
            MyEvent myEvent = new MyEvent();
            myEvent.Progress += MyEventAction_Sub1;
            myEvent.Progress += MyEventAction_Sub2;
            myEvent.Invoke();
        }

    }

    #region 类

    //类声明
    public class MyBaseClass
    {

    }
    //类继承
    public class MyClass : MyBaseClass
    {
        public string p1 { get; set; }
    }
    #endregion

    #region 结构

    public struct MyStatic
    {
        public MyStatic(string p1, string p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        public string p1 { get; set; }
        public string p2 { get; set; }
    }

    #endregion

    #region 接口
    //接口定义
    public interface IMyInterface1
    {
        string p1 { get; set; }
    }
    public interface IMyInterface2
    {
        string p2 { get; set; }
    }
    //接口实现
    public class MyInterfaceRealize : IMyInterface1, IMyInterface2
    {
        public string p1 { get; set; }
        //显示实现
        string IMyInterface2.p2 { get; set; }
    }
    #endregion

    #region 事件
    public class MyEvent
    {
        //事件命名以委托
        public event Func<object, object> Progress;
        public string p1 { get; set; }
        //调用事件
        public void Invoke()
        {
            Progress?.Invoke(this);
        }
    }

    #endregion

    #region 属性
    public class MyProperty
    {
        //普通属性
        private string p1;
        public string P1 { get { return p1; } set { p1 = value; } }
        //自动属性
        public string p2 { get; set; }
        //属性赋默认值
        public string p3 { get; set; } = "默认值";
        //只读默认值属性
        public string p4 { get; } = "默认值";
        //属性实际上是有包含一个get方法和一个set方法
        public string p5 { get { return "p5的值"; } set { p1 = value; } }
        //对外只读对内修改
        public string p6 { get; private set; }

    }

    #endregion

    #region 委托


    #endregion
}
