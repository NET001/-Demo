using System;
using System.Threading.Tasks;
using System.IO;

namespace async_await
{
    /*
     异步方法可以返回值有void、Task、Task<T>
     
     */
    class Program
    {
        static void Main(string[] args)
        {
            new Obj2().Init();
        }
    }
    public class Obj2
    {
        public async void Init()
        {
            await Method1();
            Method2();
            Console.ReadKey();
        }
        public async Task Method1()
        {
            await Task.Run(() =>
            {
                Task.Delay(1000);
                for (int i = 0; i < 50; i++)
                {
                    Console.WriteLine("1111111");
                }
            });
        }
        public Task Method2()
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    Console.WriteLine("22222222");
                }
            });
        }
    }
    public class Obj
    {
        public async void Iint()
        {
            await T_Fun1();
            await T_Fun2();
        }
        public async void V_Fun1()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("V_Fun1");
            }
        }

        public async void V_Fun2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("V_Fun2");
            }
        }
        public Task T_Fun1()
        {
            Task restul = Task.Run(() =>
             {

                 for (int i = 0; i < 10; i++)
                 {
                     Console.WriteLine("T_Fun1");
                 }
             });
            return restul;
        }
        public Task T_Fun2()
        {
            Task restul = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("T_Fun2");
                }
            });
            return restul;
        }
    }
}