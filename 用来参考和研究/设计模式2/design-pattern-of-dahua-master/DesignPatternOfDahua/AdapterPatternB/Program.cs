using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPatternB
{
    /// <summary>
    /// 适配器模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Player player;
            player = new GuardPlayer("宫城良田");
            player.Attack();
            player.Defense();

            player = new ForwardPlayer("樱木花道");
            player.Attack();
            player.Defense();

            player = new TranslatePerson("麦迪");
            player.Attack();
            player.Defense();

            Console.ReadKey();
        }
    }
}
