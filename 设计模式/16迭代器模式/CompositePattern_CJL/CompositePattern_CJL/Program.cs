using System;
using System.Collections.Generic;

namespace CompositePattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
            Console.Read();
        }
        /// <summary>
        /// 索引器实现
        /// </summary>
        static void Demo1()
        {
            ConcreteAggregate a = new ConcreteAggregate();
            a[0] = "数据a";
            a[1] = "数据b";
            a[2] = "数据c";
            a[3] = "数据d";
            a[4] = "数据e";
            a[5] = "数据f";
            Iterator i = new ConcreteIterator(a);
            object item = i.First();
            while (!i.IsDone())
            {
                Console.WriteLine(i.CurrentItem());
                i.Next();
            }
            Iterator id = new ConcreteIteratorDesc(a);
            object itemd = i.First();
            while (!id.IsDone())
            {
                Console.WriteLine(id.CurrentItem());
                id.Next();
            }
        }
        /// <summary>
        /// 迭代器实现有防止迭代过程中修改数据的验证
        /// </summary>
        static void Demo2()
        {
            Concrete2Aggregate<string> a = new Concrete2Aggregate<string>();
            a.Add("数据a");
            a.Add("数据b");
            a.Add("数据c");
            a.Add("数据d");
            a.Add("数据e");
            a.Add("数据f");
            //获取迭代器(正序)
            Iterator2<string> iterator = a.CreateIterator();
            while (iterator.hasNext())
            {
                Console.WriteLine(iterator.CurrentItem());
                iterator.Next();
            }
            //获取迭代器倒序
            iterator = new ConcreteIterator2Desc<string>(a);
            while (iterator.hasNext())
            {
                Console.WriteLine(iterator.CurrentItem());
                iterator.Next();
            }
            //获取迭代器倒序迭代过程中修改集合
            iterator = new ConcreteIterator2Desc<string>(a);
            bool flag = true;
            try
            {
                while (iterator.hasNext())
                {
                    Console.WriteLine(iterator.CurrentItem());
                    iterator.Next();
                    if (flag)
                    {
                        flag = false;
                        a.Add("数据g");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 快照迭代器
        /// </summary>
        static void Demo3()
        {

        }
    }
}