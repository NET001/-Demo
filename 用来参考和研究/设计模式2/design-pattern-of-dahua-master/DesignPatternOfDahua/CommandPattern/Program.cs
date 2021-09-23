using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern
{
    /// <summary>
    /// 命令模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            Command command1 = new CommandA(worker);
            Command command2 = new CommandB(worker);
            Waiter waiter = new Waiter();
            waiter.Add(command1);
            waiter.Add(command2);
            waiter.Notify();
            Console.ReadKey();
        }
    }
}
