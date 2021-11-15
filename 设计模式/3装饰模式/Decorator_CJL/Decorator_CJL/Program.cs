using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Decorator_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
        }
        static void Demo1()
        {

            ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA d1 = new ConcreteDecoratorA();
            ConcreteDecoratorB d2 = new ConcreteDecoratorB();
            //往对象装饰A行为
            d1.SetComponent(c);
            //继续装饰B行为
            d2.SetComponent(d1);
            //执行
            d2.Operation();
            Console.Read();
        }
        static void Demo2()
        {

            MyFileStream fileStream = new MyFileStream();
            MyCryptoStream cryptoStream = new MyCryptoStream(fileStream);
            MyGZipStream gZipStream = new MyGZipStream(cryptoStream);
            string bt = "";
            gZipStream.Write(out bt);

            Console.WriteLine(bt);
            Console.ReadLine();
        }
    }
    abstract class MyStream
    {
        public abstract void Write(out string bt);
    }
    class MyFileStream : MyStream
    {
        public override void Write(out string bt)
        {
            bt = "获取文件中的byte";
        }
    }
    class MyCryptoStream : MyStream
    {
        MyStream myStream = null;
        public MyCryptoStream(MyStream myStream)
        {
            this.myStream = myStream;
        }
        public override void Write(out string bt)
        {
            string btt = "";
            myStream.Write(out btt);
            bt = "对(" + btt + ")进行了加密处理";
        }
    }
    class MyGZipStream : MyStream
    {
        MyStream myStream = null;
        public MyGZipStream(MyStream myStream)
        {
            this.myStream = myStream;
        }
        public override void Write(out string bt)
        {
            string btt = "";
            myStream.Write(out btt);
            bt = "对(" + btt + ")进行了压缩处理";
        }
    }
}
