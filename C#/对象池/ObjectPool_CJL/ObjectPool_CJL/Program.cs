using Microsoft.Extensions.ObjectPool;
using System;

namespace ObjectPool_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            IPooledObjectPolicy<Demo> pooledObjectPolicy = new DemoPooledObjectPolicy();
            //设置对象池策略和对象池最大对象数
            DefaultObjectPool<Demo> defaultObjectPool = new DefaultObjectPool<Demo>(pooledObjectPolicy, 2);
            //通过对象池创建对象
            Demo demo1 = defaultObjectPool.Get();
            Demo demo2 = defaultObjectPool.Get();
            Demo demo3 = defaultObjectPool.Get();

            //存入对象池
            defaultObjectPool.Return(demo1);
            defaultObjectPool.Return(demo2);
            //对象池满无法存储
            defaultObjectPool.Return(demo3);

            //4和5是拿的之前存储的
            Demo demo4 = defaultObjectPool.Get();
            Demo demo5 = defaultObjectPool.Get();
            //新new
            Demo demo6 = defaultObjectPool.Get();

            Console.WriteLine("demo1：" + demo1.Id);
            Console.WriteLine("demo2：" + demo2.Id);
            Console.WriteLine("demo3：" + demo3.Id);
            Console.WriteLine("demo4：" + demo4.Id);
            Console.WriteLine("demo5：" + demo5.Id);
            Console.WriteLine("demo6：" + demo6.Id);

            Console.ReadKey();
        }
    }
    /// <summary>
    /// 对象池对象策略类
    /// </summary>
    class DemoPooledObjectPolicy : IPooledObjectPolicy<Demo>
    {
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns></returns>
        public Demo Create()
        {
            return new Demo()
            {
                Id = Guid.NewGuid().ToString(),
                CreateTimte = DateTime.Now.ToString()
            };
        }
        /// <summary>
        /// 往容器中返回对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Return(Demo obj)
        {
            //返回true代表能够归还
            return true;
        }
    }
    public class Demo
    {
        public string Id { get; set; }
        public string CreateTimte { get; set; }
    }
}
