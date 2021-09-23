using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorationPatternB
{
    /// <summary>
    /// 装饰模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("小菜");
            DecorationA decorationA = new DecorationA();
            DecorationB decorationB = new DecorationB();
            decorationA.SetPerson(person);
            decorationB.SetPerson(decorationA);
            decorationB.Show();
            Console.ReadKey();
        }
    }
}
