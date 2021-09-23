using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{
    /// <summary>
    /// 原型模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("张三", "经理",34);
            var person2 = person.ClonePersion();
            var person3 = person2.ClonePersion();
            person.Show();
            person2.Show();
            person3.Show();
            Console.ReadKey();
        }
    }
}
